using System.Threading.Tasks;
using ShopSphere.Models;

namespace ShopSphere.Services
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(UserLoginModel loginModel);
        string GenerateJwtToken(User user);
    }
}
