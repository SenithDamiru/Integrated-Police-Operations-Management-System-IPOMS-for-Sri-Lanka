using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testproj.Models;

public class EmergencyRequest
{
    [Key]
    public int EmergencyID { get; set; }

    [Required]
    public string RequestType { get; set; }

    [Required]
    public string Location { get; set; }

    public string ContactNumber { get; set; }

    public string Description { get; set; }

    public int? CitizenID { get; set; }

    [ForeignKey("CitizenID")]
    public virtual Citizen? Citizen { get; set; }  // Make nullable

    public DateTime RequestTime { get; set; }

    public string Status { get; set; }

    public int? OfficerID { get; set; }

    [ForeignKey("OfficerID")]
    public virtual PoliceOfficer? Officer { get; set; }  // Make nullable

    public DateTime? RespondedTime { get; set; }

    public string ResolutionNotes { get; set; }
}