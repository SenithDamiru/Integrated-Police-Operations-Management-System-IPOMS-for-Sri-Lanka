namespace testproj.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace testproj.Models
    {
        public class AccidentReport
        {
            [Key]
            public int ReportID { get; set; }

            [Required]
            [ForeignKey("Citizen")]
            public int CitizenID { get; set; }

            [Required]
            [MaxLength(100)]
            public string AccidentType { get; set; }

            public string Description { get; set; }

            [Required]
            [MaxLength(255)]
            public string Location { get; set; }

            public DateTime DateReported { get; set; } = DateTime.Now;

            public bool IsResolved { get; set; } = false;

            [ForeignKey("Responder")]
            public int? ResponderID { get; set; }

            // Navigation Properties
            public virtual Citizen? Citizen { get; set; }
            public virtual PoliceOfficer? Responder { get; set; }
        }
    }
}
