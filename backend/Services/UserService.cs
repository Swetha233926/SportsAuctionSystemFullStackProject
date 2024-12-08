using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface IUserService
    {
        public Task AddUser(Users user);
        public Task<Users> GetUserByEmail(string email);
        public Task<Users> GetUserById(int id);
        public Task DeleteUser(int id);
        Task<string> GetEmailByRoleAsync(string role);

    }
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUser(Users user)
        {
            await userRepository.AddUser(user);
        }

        public async Task<Users> GetUserById(int id)
        {
            return await userRepository.GetUserById(id);
        }

        public async Task<Users> GetUserByEmail(string email)
        {
            return await userRepository.GetUserByEmail(email);
        }


        public async Task DeleteUser(int id)
        {
            await userRepository.DeleteUser(id);
        }

        public async Task<string> GetEmailByRoleAsync(string role)
        {
            // Assuming only one user per role for simplicity. 
            // If multiple users exist per role, adjust this to return a collection of emails.
            var user = await userRepository.GetUserByEmail(role);
            if (user == null || !user.IsActive)
            {
                throw new KeyNotFoundException($"Active user with role '{role}' not found.");
            }
            return user.Email;
        }

    }
}
