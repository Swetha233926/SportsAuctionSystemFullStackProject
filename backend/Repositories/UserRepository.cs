using SportsAuctionSystem.Data;
using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace SportsAuctionSystem.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(Users user);
        Task<Users> GetUserById(int id);
        Task<Users> GetUserByEmail(string email);
        Task DeleteUser(int id);
        Task<Users> GetUserByRoleAsync(string role);

    }

    public class UserRepository : IUserRepository
    {
        private readonly AuctionDbContext _context;

        public UserRepository(AuctionDbContext context)
        {
            _context = context;
        }

        // Adding a user
        public async Task AddUser(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Get user by ID
        public async Task<Users> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Get user by email
        public async Task<Users> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Delete user by ID
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
        }

        // Get user by role
        public async Task<Users> GetUserByRoleAsync(string role)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Role == role && u.IsActive);
        }

    }
}
