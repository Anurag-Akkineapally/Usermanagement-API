using System.Collections.Generic;
using System.Threading.Tasks;
using UsersApi_Real;
using UsersApi_Real.Repositories;

namespace UsersApi_Real.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return user;
        }

        public async Task<User> AddUserAsync(UserCreateDto userCreateDto)
        {
            var newUser = new User
            {
                FirstName = userCreateDto.FirstName,
                LastName = userCreateDto.LastName,
                Email = userCreateDto.Email
            };

            await _userRepository.AddUserAsync(newUser);
            return newUser;
        }

        public async Task<User> UpdateUserAsync(int id, User updatedUser)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;

            await _userRepository.UpdateUserAsync(existingUser);
            return existingUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            await _userRepository.DeleteUserAsync(user);
        }
    }

    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(UserCreateDto userCreateDto);
        Task<User> UpdateUserAsync(int id, User updatedUser);
        Task DeleteUserAsync(int id);
    }
}
