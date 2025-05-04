using System;

namespace testproj.DTOs
{
    public class PoliceOfficerDto
    {
        public int OfficerID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int StationID { get; set; }
        public string StationName { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateJoined { get; set; }
    }
}