using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Bids
    {
        [Key]
        public int BidId { get; set; } // Primary Key

        [Required(ErrorMessage = "Auction is required.")]
        [ForeignKey(nameof(Auction))]
        public int AuctionId { get; set; } // FK to Auctions

        [Required(ErrorMessage = "Player is required.")]
        [ForeignKey(nameof(Players))]
        public int PlayerId { get; set; } // FK to Players

        [Required(ErrorMessage = "Bid amount is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Bid amount must be greater than 0.")]
        public decimal BidAmount { get; set; }

        [Required(ErrorMessage = "Team is required.")]
        [ForeignKey (nameof(Team))]
        public int TeamId { get; set; } // FK to Teams

        [Required(ErrorMessage = "Bid time is required.")]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid date-time format.")]
        public DateTime BidTime { get; set; }= DateTime.Now;

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Active|Outbid|Winning|Won", ErrorMessage = "Status must be 'Active', 'Outbid', or 'Winning' or 'Won'.")]
        public string Status { get; set; }

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
