using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class PerformanceReports
    {
        [Key]
        public int ReportId { get; set; } // Primary Key

        [Required(ErrorMessage = "Player ID is required.")]
        [ForeignKey(nameof(Players))]
        public int PlayerId { get; set; } // FK to Players

        [Required(ErrorMessage = "Match date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime MatchDate { get; set; }

        [Required(ErrorMessage = "Tournament name is required.")]
        [StringLength(100, ErrorMessage = "Tournament name cannot exceed 100 characters.")]
        public string Tournament { get; set; }

        [Required(ErrorMessage = "Performance details are required.")]
        public string PerformanceDetails { get; set; } // Can store detailed stats or text

        [Required(ErrorMessage = "Analyst ID is required.")]
        [ForeignKey(nameof(Users.UserId))]
        public int AnalystId { get; set; } // FK to Users (Analyst)

        [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10.")]
        public int Rating { get; set; } // Rating of player performance

        [JsonIgnore]
        public Players Player { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Users Analyst { get; set; }
    }

}
