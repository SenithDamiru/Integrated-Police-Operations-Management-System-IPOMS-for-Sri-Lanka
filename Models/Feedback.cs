using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testproj.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required]
        [MaxLength(50)] // Matches database column length
        public string FeedbackType { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Comments { get; set; }

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
