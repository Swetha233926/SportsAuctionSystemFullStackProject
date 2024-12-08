using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace SportsAuctionSystem.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50, ErrorMessage = "Username cannot exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string PasswordHash { get; set; } 

        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(?i)(Admin|Auctioneer|PlayerAgent|TeamManager|Analyst|Viewer)$",ErrorMessage = "Role must be one of the predefined roles.")]
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;
        
    }
}
