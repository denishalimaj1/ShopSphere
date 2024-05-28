using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopSphere.Data;
using ShopSphere.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public class AuthService : IAuthService
    {
        private readonly ShopSphereContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ShopSphereContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User?> AuthenticateAsync(UserLoginModel loginModel)
{
    var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginModel.Username);

    if (user == null || !VerifyPasswordHash(loginModel.Password, user.PasswordHash))
        return null;

    return user;
}

   public string GenerateJwtToken(User user)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]!); 
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Username)
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    
    return "Bearer " + tokenHandler.WriteToken(token);
}


        private bool VerifyPasswordHash(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
