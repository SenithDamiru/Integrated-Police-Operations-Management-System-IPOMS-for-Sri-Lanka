using System;

namespace testproj.Models
{
    public class PoliceOfficerInputDto
    {
        public int? OfficerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public int RoleID { get; set; }
        public int StationID { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}