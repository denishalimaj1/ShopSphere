using Microsoft.EntityFrameworkCore;
using ShopSphere.Data;
using ShopSphere.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopSphere.Services
{
    public class UserService : IUserService
    {
        private readonly ShopSphereContext _context;

        public UserService(ShopSphereContext context)
        {
            _context = context;
        }

        public async Task<UserRegistrationModel> RegisterAsync(User userDto)
        {
            ValidateUser(userDto);

            if (await UsernameExists(userDto.Username))
            {
                throw new Exception("Username is already taken.");
            }

            if (await EmailExists(userDto.Email))
            {
                throw new Exception("Email is already registered.");
            }

            var newUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.PasswordHash),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DateCreated = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            var registeredUser = new UserRegistrationModel
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                DateCreated = newUser.DateCreated
            };

            return registeredUser;
        }

        public async Task<UserRegistrationModel> UpdateUserAsync(int id, UpdateUserModel userDto)
        {
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            if (!string.IsNullOrWhiteSpace(userDto.Username))
            {
                existingUser.Username = userDto.Username;
            }

            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                existingUser.Email = userDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(userDto.FirstName))
            {
                existingUser.FirstName = userDto.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(userDto.LastName))
            {
                existingUser.LastName = userDto.LastName;
            }

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                existingUser.PasswordHash = HashPassword(userDto.Password);
            }

            ValidateUser(existingUser);

            await _context.SaveChangesAsync();

            var updatedUser = new UserRegistrationModel
            {
                Id = existingUser.Id,
                Username = existingUser.Username,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                LastName = existingUser.LastName,
                DateCreated = existingUser.DateCreated
            };

            return updatedUser;
        }

        private void ValidateUser(User userDto)
        {
            var validationContext = new ValidationContext(userDto);
            Validator.ValidateObject(userDto, validationContext, validateAllProperties: true);
        }

        private async Task<bool> UsernameExists(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
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
         public async Task DeleteUserAsync(int id)
        {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            throw new Exception("User not found.");
        }

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
    }

        public async Task<GetUserDetails?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return null;
            }

            return new GetUserDetails
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateCreated = user.DateCreated
            };
        }
        public async Task<IEnumerable<GetUserDetails?>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(user => new GetUserDetails
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateCreated = user.DateCreated
                })
                .ToListAsync();
        }
    }
}
