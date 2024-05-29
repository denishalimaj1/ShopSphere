using System.Threading.Tasks;
using ShopSphere.Models;

namespace ShopSphere.Services
{
    public interface IUserService
    {
        Task<UserRegistrationModel> RegisterAsync(User userDto);
        Task<UserRegistrationModel> UpdateUserAsync(int id, UpdateUserModel userDto);
         Task DeleteUserAsync(int id); 
         Task<GetUserDetails?> GetUserByIdAsync(int id);

          Task<IEnumerable<GetUserDetails?>> GetAllUsersAsync();
        
    }
}
