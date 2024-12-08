using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Finance
    {
        [Key]
        public int FinanceId { get; set; } // Primary Key

        [Required(ErrorMessage = "Team ID is required.")]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; } // FK to Teams

        [Required(ErrorMessage = "Transaction type is required.")]
        [RegularExpression("Purchase|Fee|Refund", ErrorMessage = "Transaction type must be 'Purchase', 'Fee', or 'Refund'.")]
        public string TransactionType { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Transaction date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime Date { get; set; }

        [StringLength(250, ErrorMessage = "Details cannot exceed 250 characters.")]
        public string Details { get; set; } // Optional description of the transaction

        [JsonIgnore]
        [ValidateNever]
        public Team Team { get; set; }
    }
}
