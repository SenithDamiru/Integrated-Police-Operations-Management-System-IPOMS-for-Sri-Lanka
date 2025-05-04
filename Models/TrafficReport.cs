namespace testproj.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TrafficReport
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int CitizenID { get; set; }

        [Required]
        [StringLength(100)]
        public string TrafficType { get; set; } // Type of traffic issue (e.g., Accident, Heavy Congestion)

        [Required]
        [StringLength(255)]
        public string Location { get; set; } // Where the traffic issue occurred

        [Required]
        [Range(1, 5)]
        public int SeverityLevel { get; set; } // Severity level (1 = Low, 5 = Severe)

        public DateTime DateReported { get; set; } = DateTime.Now; // Auto-filled with current timestamp

        public bool IsResolved { get; set; } = false; // Default to false (not resolved)

        public int? ResponderID { get; set; } // Nullable, police officer handling the case

        // Navigation Properties
        [ForeignKey("CitizenID")]
        public Citizen? Citizen { get; set; }

        [ForeignKey("ResponderID")]
        public PoliceOfficer? Responder { get; set; }
    }

}
