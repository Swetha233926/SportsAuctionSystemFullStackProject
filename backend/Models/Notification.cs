using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; } // Primary Key

        [Required(ErrorMessage = "Notification type is required.")]
        [RegularExpression("AuctionStart|BidUpdate|BudgetUpdate|PlayerAvailability",
            ErrorMessage = "Notification type must be 'AuctionStart', 'BidUpdate', 'BudgetUpdate', or 'PlayerAvailability'.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        [StringLength(500, ErrorMessage = "Message cannot exceed 500 characters.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Timestamp is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date-time format.")]
        public DateTime Timestamp { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; } // FK to Users

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Sent|Read|Pending", ErrorMessage = "Status must be 'Sent', 'Read', or 'Pending'.")]
        public string Status { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; }
    }
}
