using SportsAuctionSystem.Models;
using Microsoft.EntityFrameworkCore;
using SportsAuctionSystem.Data;

namespace SportsAuctionSystem.Repositories
{
    public interface INotificationRepository
    {
        Task AddNotificationAsync(Notification notification);
    }

    public class NotificationRepository : INotificationRepository
    {
        private readonly AuctionDbContext _context;

        public NotificationRepository(AuctionDbContext context)
        {
            _context = context;
        }

        public async Task AddNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification); // Add to Notifications table
            await _context.SaveChangesAsync(); // Commit the changes to the database
        }
    }
}
