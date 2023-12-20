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
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel newCustomer)
        {
            try
            {
                string salt = Guid.NewGuid().ToString("N");
                var updatedCustomer = new CustomerModel
                {
                    Email = newCustomer.Email,
                    Name = newCustomer.Name,
                    Username = newCustomer.Username,
                    CurrencyId = newCustomer.CurrencyId,
                    PasswordHash = HashPassword(newCustomer.PasswordHash, salt),
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
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginModel logCustomer)
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                var filteredCustomer = customers.FirstOrDefault(c =>
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Audience"],
                claims: new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Email,user.Email)
                },
                expires: DateTime.Now.AddHours(8), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

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
