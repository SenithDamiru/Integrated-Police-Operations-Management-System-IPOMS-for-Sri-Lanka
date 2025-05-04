using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testproj.Models
{
    public class PoliceOfficer
    {
        [Key]
        public int OfficerID { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } // Officer's Full Name

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } // Email for login/communication

        [Phone]
        [StringLength(15)]
        public string? PhoneNumber { get; set; } // Contact number (nullable)

        [Required]
        public int RoleID { get; set; } // Foreign key for Role

        [ForeignKey("RoleID")]
        public virtual PoliceRole Role { get; set; } // Navigation property for PoliceRoles

        [Required]
        public int StationID { get; set; } // Foreign key for Station

        [ForeignKey("StationID")]
        public virtual PoliceStation Station { get; set; } // Navigation property for PoliceStations

        [Required]
        public DateTime DateJoined { get; set; } = DateTime.Now; // Date when officer joined, default to current date

        [Required]
        public bool IsActive { get; set; } = true; // Active status, default to true

        [Required]
        public string PasswordHash { get; set; } // Password hash for authentication
    }
}