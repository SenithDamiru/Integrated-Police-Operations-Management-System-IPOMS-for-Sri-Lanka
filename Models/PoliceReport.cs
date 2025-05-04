using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testproj.Models;

namespace testproj.Models
{
    public class PoliceReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CitizenID { get; set; }

        [Required]
        [StringLength(255)]
        public string ReportType { get; set; }

        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [Required]
        public DateTime DateRequested { get; set; } = DateTime.Now;

        // Navigation property for Citizen
        [ForeignKey("CitizenID")]
        public Citizen? Citizen { get; set; }
    }
}