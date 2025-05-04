using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testproj.Models
{
    public class CybercrimeReport
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int CitizenID { get; set; } // Foreign key

        [Required]
        [StringLength(100)]
        public string ReportType { get; set; } // Type of cybercrime

        [Required]
        public string Description { get; set; } // Detailed description

        public string EvidenceURL { get; set; } // URL or path for evidence

        [Required]
        [StringLength(50)]
        public string ReportStatus { get; set; } = "Pending"; // Default status

        public DateTime DateReported { get; set; } = DateTime.Now;

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public int? AssignedOfficerID { get; set; } // Nullable foreign key

        // Navigation properties
        [ForeignKey("CitizenID")]
        public Citizen? Citizen { get; set; }

        [ForeignKey("AssignedOfficerID")]
        public PoliceOfficer? AssignedOfficer { get; set; }
    }
}