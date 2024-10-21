using AutoMapper;
using Bank.ClientAPI.Models;
using Bank.UseCases.Customer.CommandCreateCustomer;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly RoleManager<Customer> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AuthController(UserManager<Customer> userManager, RoleManager<Customer> roleManager, IConfiguration configuration, IMapper mapper, IMediator mediator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateCustomerCommand command)
        {
            Customer customer = _mapper.Map<Customer>(command);
            var response = await _userManager.CreateAsync(customer);
            if (response.Succeeded)
            {
                return Ok(new { message = "Registration succesfull" });
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            Customer? customer = await _userManager.FindByNameAsync(login.UserName);
            if (customer != null && await _userManager.CheckPasswordAsync(customer, login.Password)) {
                var userRoles = await _userManager.GetRolesAsync(customer);
                var authClaims = new List<Claim>() { 
                    new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    expires: DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                    SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new TokenResponse()
                {
                    TokenType = "Bearer",
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpiresIn = int.Parse(_configuration["Jwt:ExpiryMinutes"]!),
                    RefreshToken = ""
                });
            }
            return BadRequest();
            
        }
    }
}
