using System.ComponentModel.DataAnnotations;

namespace testproj.Models
{
    public class Admin
    {
        public int AdminId { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }


}
