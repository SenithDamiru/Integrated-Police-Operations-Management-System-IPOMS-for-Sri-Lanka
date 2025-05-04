using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using testproj.Models;

public class Fine
{
    [Key]
    public int FineID { get; set; }  // Primary Key

    [Required]
    [ForeignKey("Citizen")]
    public int CitizenID { get; set; }

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal FineAmount { get; set; }  // Fine amount

    [Required]
    [StringLength(255)]
    public string FineReason { get; set; }  // Reason for fine

    public DateTime IssueDate { get; set; } = DateTime.Now;  // Default to current date

    public DateTime? DueDate { get; set; }  // Due date for payment

    [StringLength(50)]
    public string Status { get; set; } = "Unpaid";  // Default status is 'Unpaid'

    public DateTime? PaymentDate { get; set; }  // When payment was made

    [StringLength(100)]
    public string? TransactionID { get; set; }  // Payment transaction ID (Stripe, etc.)

    // Navigation Property (Nullable)
    [ForeignKey("CitizenID")]
    public Citizen? Citizen { get; set; }
}