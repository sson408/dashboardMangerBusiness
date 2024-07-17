using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using dashboardManger.Data;
using dashboardManger.Models;
using System.Security.Cryptography;


namespace dashboardManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IConfiguration _configuration;
        private static List<RefreshToken> refreshTokens = new List<RefreshToken>();

        public AuthController(MyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegister request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                return BadRequest(new ApiResponse<string>(400, "Username already exists", null));
            }

            var user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new ApiResponse<string>(200, "User registered successfully", null));
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new ApiResponse<string>(401, "Invalid username or password", null));
            }

            var token = GenerateJwtToken(user.Username);
            var refreshToken = GenerateRefreshToken();
            refreshTokens.Add(refreshToken);

            var response = new
            {
                Token = token,
                RefreshToken = refreshToken.Token
            };

            return Ok(new ApiResponse<object>(200, "Login successful", response));
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenRequest tokenRequest)
        {
            var principal = GetPrincipalFromExpiredToken(tokenRequest.Token);
            var username = principal.Identity.Name;
            var savedRefreshToken = refreshTokens.SingleOrDefault(rt => rt.Token == tokenRequest.RefreshToken);

            if (savedRefreshToken == null || savedRefreshToken.ExpiryDate <= DateTime.UtcNow)
            {
                return Unauthorized(new ApiResponse<string>(401, "Invalid refresh token", null));
            }

            var newJwtToken = GenerateJwtToken(username);
            var newRefreshToken = GenerateRefreshToken();
            refreshTokens.Remove(savedRefreshToken);
            refreshTokens.Add(newRefreshToken);

            var response = new
            {
                Token = newJwtToken,
                RefreshToken = newRefreshToken.Token
            };

            return Ok(new ApiResponse<object>(200, "Token refreshed successfully", response));
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, username)
            }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiresInMinutes"])),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiryDate = DateTime.UtcNow.AddDays(int.Parse(_configuration["Jwt:RefreshTokenExpiresInDays"]))
            };
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, 
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }



    public class UserRegister
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class TokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
