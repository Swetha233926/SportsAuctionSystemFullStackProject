using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Numerics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SportsAuctionSystem.Models
{
    public class Contracts
    {
        [Key]
        public int ContractId { get; set; } // Primary Key

        [Required(ErrorMessage = "Player ID is required.")]
        [ForeignKey(nameof(Players))]
        public int PlayerId { get; set; } // FK to Players

        [Required(ErrorMessage = "Team ID is required.")]
        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; } // FK to Teams

        [Required(ErrorMessage = "Contract start date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid start date format.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Contract end date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid end date format.")]
        //[CustomValidation(typeof(System.Diagnostics.Contracts.Contract), nameof(ValidateContractDates))]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(1000, 10000000, ErrorMessage = "Salary must be between 1,000 and 10,000,000.")]
        public decimal Salary { get; set; }

        [Range(0, 1000000, ErrorMessage = "Bonuses cannot exceed 1,000,000.")]
        public decimal? Bonuses { get; set; } // Nullable, as not all contracts have bonuses

        [Required(ErrorMessage = "Status is required.")]
        [RegularExpression("Active|Expired|Terminated", ErrorMessage = "Status must be 'Active', 'Expired', or 'Terminated'.")]
        public string Status { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Players Player { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Team Team { get; set; }
    }
}
