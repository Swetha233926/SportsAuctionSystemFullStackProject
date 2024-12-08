using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Auction
    {
        [Key]
        public int AuctionId { get; set; }

        [Required(ErrorMessage = "Auction Name is required.")]
        public string AuctionName { get; set; }

        [Required(ErrorMessage = "Auction date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Sport is required.")]
        public string Sport { get; set; }

        [Required(ErrorMessage = "Auctioneer is required.")]
        [ForeignKey(nameof(Users.UserId))]
        public int AuctioneerId { get; set; } // fk to users

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        [CustomValidation(typeof(Auction), nameof(ValidateTimeRange))]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Upcoming|Ongoing|Completed", ErrorMessage = "Status must be 'Upcoming', 'Ongoing', or 'Completed'.")]
        public string Status { get; set; }

        public static ValidationResult ValidateTimeRange(TimeSpan endTime, ValidationContext context)
        {
            var instance = context.ObjectInstance as Auction;
            if (instance != null && instance.StartTime >= endTime)
            {
                return new ValidationResult("End time must be after start time.");
            }
            return ValidationResult.Success;
        }

        // Navigation Property to User (Auctioneer)
        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; } // The auctioneer managing this auction
    }
}
