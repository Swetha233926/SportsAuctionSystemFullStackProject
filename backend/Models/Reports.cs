using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SportsAuctionSystem.Models
{
    public class Reports
    {
        [Key]
        public int ReportId { get; set; } // Primary Key

        [Required(ErrorMessage = "Report type is required.")]
        [RegularExpression("AuctionOutcome|PlayerStatistics|BiddingTrends|TeamSpending",
            ErrorMessage = "Report type must be 'AuctionOutcome', 'PlayerStatistics', 'BiddingTrends', or 'TeamSpending'.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Generated date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime GeneratedDate { get; set; }

        [Required(ErrorMessage = "Report data is required.")]
        public string Data { get; set; } // Could store JSON data or CSV for the report details

        [Required(ErrorMessage = "Created by is required.")]
        [ForeignKey(nameof(CreatedByUser))]
        public int CreatedBy { get; set; } // FK to Users (Admin or Analyst who created the report)
        [JsonIgnore]
        public Users CreatedByUser { get; set; }
    }
}
