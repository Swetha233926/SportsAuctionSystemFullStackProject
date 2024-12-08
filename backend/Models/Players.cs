using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Players
    {
        [Key]
        public int PlayerId { get; set; } 

        [Required(ErrorMessage = "Player name is required.")]
        [StringLength(100, ErrorMessage = "Player name cannot exceed 100 characters.")]
        public string PlayerName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Sport is required.")]
        public string Sport { get; set; } 

        [Range(15, 50, ErrorMessage = "Age must be between 15 and 50 years.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country name cannot exceed 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public string Position { get; set; } 

        [Range(10000, 100000000, ErrorMessage = "Base price must be between 10,000 and 100,000,000.")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Skills are required.")]
        public string Skills { get; set; } 

        public string PerformanceStats { get; set; }

        [Required(ErrorMessage = "Agent is required.")]
        public int AgentId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Active|Inactive", ErrorMessage = "Status must be either 'Active' or 'Inactive'.")]
        public string Status { get; set; } = "Inactive";
        public decimal CurrentBid { get; set; }

        // Navigation Property
        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; } 
    }
}
