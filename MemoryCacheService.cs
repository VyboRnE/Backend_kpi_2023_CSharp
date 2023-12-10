using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using LabBackend.Models;

namespace LabBackend
{
    public class MemoryCacheService<T>
    {
        private readonly IMemoryCache _memoryCache;
        public int index = 0;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IEnumerable<T> GetAll()
        {
            return _memoryCache.TryGetValue(GetKey(), out var value) ? (IEnumerable<T>)value : null;
        }

        public T GetById(int id)
        {
            var items = GetAll();
            return items.OfType<T>().FirstOrDefault(item => GetId(item) == id);
        }

        public void Add(T item)
        {
            var items = GetAll()?.ToList() ?? new List<T>();
            items.Add(item);
            index++;
            _memoryCache.Set(GetKey(), items, new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.NeverRemove
            });
        }

        public void DeleteById(int id)
        {
            var items = GetAll()?.ToList() ?? new List<T>();
            var itemToRemove = items.FirstOrDefault(item => GetId(item) == id);

            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);

                _memoryCache.Set(GetKey(), items, new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.NeverRemove
                });
            }
        }

        private string GetKey()
        {
            // Use a unique key based on the type T
            return $"{typeof(T).Name}_CacheKey";
        }

        private int GetId(T item)
        {
            // Adjust this method to get the ID from your model
            // This assumes that your model has a property named "Id"
            return item.GetType().GetProperty("Id")?.GetValue(item, null) as int? ?? 0;
        }
    }
}
