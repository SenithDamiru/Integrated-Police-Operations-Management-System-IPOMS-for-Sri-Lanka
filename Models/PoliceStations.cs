using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using testproj.Models;

namespace testproj.Models
{
    public class PoliceStation
    {
        [Key]
        public int StationID { get; set; } // Primary Key

        [Required]
        [StringLength(100)]
        public string StationName { get; set; } // Name of the station

        [StringLength(200)]
        public string? Location { get; set; } // Location of the station (nullable)

        // Navigation property for Police Officers assigned to this station
        public virtual ICollection<PoliceOfficer> PoliceOfficers { get; set; }
    }
}