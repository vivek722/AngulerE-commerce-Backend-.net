using E_commerce.Domain.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngulerE_commerce.Controllers
{
    public class UserAuthenticationController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserAuthenticationController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> register([FromBody] UserRegister usermodel)
        {
            var userExist = await _userManager.FindByNameAsync(usermodel.username);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "error", Message = "user alredy Exists" });
            }
            IdentityUser use = new()
            {
                Email = usermodel.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = usermodel.username
            };

            var result = await _userManager.CreateAsync(use, usermodel.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new UserResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        
            return Ok(new UserResponse { Status = "Success", Message = "User created successfully!" });
            }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin usermodel)
        {
            var user = await _userManager.FindByNameAsync(usermodel.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, usermodel.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, $"{user.Email}"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
         return Unauthorized();
        }
        [HttpGet]
        public async Task<IActionResult> TotalUser()
        {
            var users = await _userManager.Users.AsNoTracking().ToListAsync();
            var TotalUsers = users.Count();
            if(TotalUsers > 0)
            {
                return Ok(TotalUsers);
            }
            return Ok(TotalUsers);

        }

            private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
    }
}
