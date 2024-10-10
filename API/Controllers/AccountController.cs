using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Account;
using Microsoft.Extensions.Configuration;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Jose;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;


        public AccountController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration) 
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
    
        }
      
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole("User"));
                }


                if (string.IsNullOrEmpty(model.RoleName)) { model.RoleName = "User"; }

                await _userManager.AddToRoleAsync(user, model.RoleName);
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);
                var tokenHandler = new JwtSecurityTokenHandler();

                // Convert roles to string array
                var rolesArray = roles.ToArray();

                var tokenString = GenerateToken(user, rolesArray);

                AddUserLoginInfo(user, tokenString);
                // Decode the token to extract username and expiration date
                var token = tokenHandler.ReadJwtToken(tokenString);
                var username = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var expirationDate = token.ValidTo;


                return Ok(new { Token = tokenString, Username = user.UserName, Expiration = expirationDate });
            }

            return Unauthorized();
        }

        [HttpPost("create-role")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto model)
        {
            if (!await _roleManager.RoleExistsAsync(model.RoleName))
            {
                var role = new IdentityRole { Name = model.RoleName };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return Ok(new { Message = "Role created successfully" });
                }

                return BadRequest(result.Errors);
            }

            return BadRequest(new { Message = "Role already exists" });
        }
        private string GenerateToken(IdentityUser user, string[] roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Read JWT settings from appsettings.json
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);
            var tokenLifeTime = (double)Convert.ChangeType(_configuration["JwtSettings:TokenLifeTime"], typeof(double));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Role,roles[0])
                }),
                NotBefore = DateTime.UtcNow, 
                Expires = DateTime.UtcNow.AddMinutes(tokenLifeTime),
                Issuer = jwtSettings["ValidIssuer"],
                Audience = jwtSettings["ValidAudience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
        private async Task AddUserLoginInfo(IdentityUser user, string tokenString)
        {
            var loginInfo = new UserLoginInfo(DateTime.Now.ToString(), tokenString, user.UserName);
            await _userManager.AddLoginAsync(user, loginInfo);
        }
    }
}
