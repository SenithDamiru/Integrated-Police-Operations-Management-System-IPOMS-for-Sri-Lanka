using Microsoft.EntityFrameworkCore;

using testproj.Models;
using testproj.Models.testproj.Models;

namespace testproj.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }  // Admin Table
        public DbSet<Citizen> Citizens { get; set; } // Citizens Table

        public DbSet<Complaint> Complaints { get; set; }

        public DbSet<CybercrimeReport> CybercrimeReports { get; set; }
       
        public DbSet<PoliceOfficer> PoliceOfficers { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<SecurityRequest> SecurityRequests { get; set; }

        public DbSet<AccidentReport> AccidentReports { get; set; }

        public DbSet<RobberyReport> RobberyReports { get; set; }

        public DbSet<TrafficReport> TrafficReports { get; set; }

        public DbSet<PoliceReport> PoliceReports { get; set; }

        public DbSet<Fine> Fines { get; set; }

        public DbSet<EmergencyRequest> EmergencyRequests { get; set; }

        public DbSet<PoliceRole> PoliceRoles { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }


    }

}


