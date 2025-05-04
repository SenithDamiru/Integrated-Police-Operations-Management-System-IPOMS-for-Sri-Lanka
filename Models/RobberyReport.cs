using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testproj.Models
{

    public class RobberyReport
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int CitizenID { get; set; }

        [Required]
        [StringLength(100)]
        public string RobberyType { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        public DateTime RobberyDate { get; set; } // Date when the robbery occurred

        public string StolenItems { get; set; } // List of stolen/misplaced items

        public DateTime DateReported { get; set; } = DateTime.Now; // Default to current timestamp

        public bool IsResolved { get; set; } = false; // Default false (not resolved)

        public int? ResponderID { get; set; } // Nullable, police officer handling the case

        // Navigation Properties
        [ForeignKey("CitizenID")]
        public Citizen? Citizen { get; set; }

        [ForeignKey("ResponderID")]
        public PoliceOfficer? Responder { get; set; }
    }
}
