namespace testproj.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public int CitizenID { get; set; }
        public string ComplaintTitle { get; set; }
        public string ComplaintDescription { get; set; }
        public string ComplaintStatus { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
