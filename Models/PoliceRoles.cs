using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using testproj.Models;

namespace testproj.Models
{
    public class PoliceRole
    {
        [Key]
        public int RoleID { get; set; } // Primary Key

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } // Name of the role, e.g., Admin, Officer, etc.

        // Navigation property for Police Officers assigned to this role
        public virtual ICollection<PoliceOfficer> PoliceOfficers { get; set; }
    }
}