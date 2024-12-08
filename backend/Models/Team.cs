using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; } // Primary Key

        [Required(ErrorMessage = "Team name is required.")]
        [StringLength(100, ErrorMessage = "Team name cannot exceed 100 characters.")]
        public string TeamName { get; set; }

        [ForeignKey(nameof(User))]
        public int ManagerId { get; set; } // Foreign Key to Users table

        [Required(ErrorMessage = "Sport is required.")]
        [StringLength(50, ErrorMessage = "Sport cannot exceed 50 characters.")]
        public string Sport { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Budget cannot be negative.")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Region is required.")]
        [StringLength(50, ErrorMessage = "Region cannot exceed 50 characters.")]
        public string Region { get; set; }

        public int RosterSize { get; set; } // Number of players in the team

        [Range(0, double.MaxValue, ErrorMessage = "Expenditures cannot be negative.")]
        public decimal TotalExpenditure { get; set; }


        // Navigation Property
        [JsonIgnore]
        [ValidateNever]
        public Users User { get; set; } // Manager relationship
    }

}