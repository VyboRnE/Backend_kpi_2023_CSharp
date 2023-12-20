using LabBackend;
using LabBackend.Business.Interfaces;
using LabBackend.Business.Models;
using LabBackend.Business.Validation;
using LabBackend.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace LabBackeend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        public CustomerController(ICustomerService customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }

        //user/<user_id>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetById(int id)
        {
            try
            {
                var customer = await _customerService.GetByIdAsync(id);
                return Ok(customer);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> Get()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                return Ok(customers);
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //user
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] CustomerModel customer)
        {
            try
            {
                string salt = Guid.NewGuid().ToString("N");
                var updatedCustomer = new CustomerModel
                {
                    Email = customer.Email,
                    Name = customer.Name,
                    Username = customer.Username,
                    CurrencyId = customer.CurrencyId,
                    PasswordHash = HashPassword(customer.PasswordHash, salt),
                    Salt = salt
                };
                await _customerService.AddAsync(updatedCustomer);
                var token = GenerateJwtToken(updatedCustomer);
                return Ok(new {Token = token});
            }
            catch (ShopException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel logCustomer)
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                var filteredCustomer = customers.SingleOrDefault(c =>
                    c.Username == logCustomer.Username &&
                    c.PasswordHash == HashPassword(logCustomer.PasswordHash, c.Salt));
                if(filteredCustomer is null) { return BadRequest("No user found. Username or password doesn't match."); }
                var token = GenerateJwtToken(filteredCustomer);
                return Ok(new { Token = token });
            }
            catch (ShopException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //user/<user_id>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _customerService.DeleteByIdAsync(id);
                return Ok($"Customer {id} was deleated.");
            }
            catch (ShopException ex)
            {
                return NotFound(ex.Message);
            }
        }

        private string GenerateJwtToken(CustomerModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string HashPassword(string password, string salt)
        {
            // Generate a 128-bit salt using a secure PRNG
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashed;
        }

    }
}
