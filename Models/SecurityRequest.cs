using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testproj.Models;

namespace testproj.Models
{
    public class SecurityRequest
    {
        [Key]
        public int RequestID { get; set; }  // Auto-incrementing unique ID for each request

        [Required]
        public int CitizenID { get; set; }  // Foreign key to identify the requesting citizen

        [Required]
        [StringLength(500)]  // Maximum length for the reason
        public string RequestReason { get; set; }  // Reason for requesting security

        [Required]
        [StringLength(255)]  // Maximum length for the location
        public string Location { get; set; }  // Location where security is needed

        [Required]
        public DateTime DateRequested { get; set; } = DateTime.Now;  // Timestamp of when the request was created

        [Required]
        [DataType(DataType.Date)]  // Ensures only the date is accepted
        public DateTime SecurityDate { get; set; }  // The specific date when security is needed

        [StringLength(1000)]  // Maximum length for description
        public string Description { get; set; }  // Additional description about the request

        [StringLength(50)]
        public string Status { get; set; } = "Pending";  // Status of the request (e.g., Pending, Approved, Rejected)

        public int? AssignedOfficerID { get; set; }  // Officer assigned to fulfill the request (optional)

        // Navigation property to the Citizens table
        [ForeignKey("CitizenID")]
        public virtual Citizen? Citizen { get; set; }

        // Navigation property to the PoliceOfficers table (optional)
        [ForeignKey("AssignedOfficerID")]
        public virtual PoliceOfficer? AssignedOfficer { get; set; }
    }

}
