using SendGrid.Helpers.Mail;
using SendGrid;
using SportsAuctionSystem.Models;
using SportsAuctionSystem.Repositories;

namespace SportsAuctionSystem.Services
{
    public interface INotificationService
    {
        Task NotifyTeamManager(int userId, string managerEmail, string teamName, decimal bidAmount, string playerName);
        Task NotifyPlayer(int userId, string playerEmail, string teamName, decimal bidAmount);
    }

    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly SendGridClient _sendGridClient;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public NotificationService(INotificationRepository notificationRepository, IConfiguration configuration)
        {
            _notificationRepository = notificationRepository;
            _sendGridClient = new SendGridClient(configuration["SendGrid:ApiKey"]);
            _fromEmail = configuration["SendGrid:FromEmail"];
            _fromName = configuration["SendGrid:FromName"];
        }

        private async Task SendEmail(string toEmail, string subject, string body)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = subject,
                PlainTextContent = body,
                HtmlContent = body
            };
            message.AddTo(toEmail);

            var response = await _sendGridClient.SendEmailAsync(message);
            if (response.StatusCode != System.Net.HttpStatusCode.OK &&
                response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
            }
        }

        public async Task NotifyTeamManager(int userId, string managerEmail, string teamName, decimal bidAmount, string playerName)
        {
            var subject = $"Bid Won: {teamName}";
            var body = $"Congratulations! Your team '{teamName}' won the bid for player '{playerName}' with an amount of {bidAmount:C}.";

            // Log the notification
            var notification = new Notification
            {
                Type = "BidUpdate",
                Message = body,
                Timestamp = DateTime.Now,
                UserId = userId,
                Status = "Sent"
            };
            await _notificationRepository.AddNotificationAsync(notification); // Corrected method name

            // Send the email
            await SendEmail(managerEmail, subject, body);
        }

        public async Task NotifyPlayer(int userId, string playerEmail, string teamName, decimal bidAmount)
        {
            var subject = $"Player Sold: {teamName}";
            var body = $"Your auction has concluded. You have been sold to team '{teamName}' for {bidAmount:C}.";

            // Log the notification
            var notification = new Notification
            {
                Type = "PlayerAvailability",
                Message = body,
                Timestamp = DateTime.Now,
                UserId = userId,
                Status = "Sent"
            };
            await _notificationRepository.AddNotificationAsync(notification); // Corrected method name

            // Send the email
            await SendEmail(playerEmail, subject, body);
        }
    }
}
