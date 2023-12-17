using System.ComponentModel.DataAnnotations;

namespace LabBackend.Data.Entities
{
    public class Currency:BaseEntity
    {
        public string Name {  get; set; }
        public decimal RateToUAH { get; set; }
    }
}

/*
 id = 1
name = "USD"
exchangeRateToUAH = 37.0m
 */
