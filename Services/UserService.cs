using UserManagmentAPI.Models;
using UserManagmentAPI.Repositories;

namespace UserManagmentAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }

    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();

        public async Task<User> GetUserByIdAsync(int id) => await _userRepository.GetByIdAsync(id);

        public async Task CreateUserAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user) => await _userRepository.UpdateAsync(user);

        public async Task DeleteUserAsync(int id) => await _userRepository.DeleteAsync(id);
    }
}
