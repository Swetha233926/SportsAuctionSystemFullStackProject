using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class AuctionResults
    {
        [Key]
        public int ResultId { get; set; } // Primary Key


        [Required(ErrorMessage = "Auction ID is required.")]
        [ForeignKey(nameof(Auction))]
        public int AuctionId { get; set; } // FK to Auctions

        [Required(ErrorMessage = "Player ID is required.")]
        [ForeignKey(nameof(Players))]
        public int PlayerId { get; set; } // FK to Players

        [Required(ErrorMessage = "Winning team ID is required.")]
        [ForeignKey(nameof(Team))]
        public int WinningTeamId { get; set; } // FK to Teams

        [Required(ErrorMessage = "Final price is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Final price must be greater than 0.")]
        public decimal FinalPrice { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Sold|Unsold", ErrorMessage = "Status must be either 'Sold' or 'Unsold'.")]
        public string Status { get; set; }

        // Navigation Properties
        [JsonIgnore]
        [ValidateNever]
        public Auction Auction { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Players Player { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Team Team { get; set; }
    }
}
