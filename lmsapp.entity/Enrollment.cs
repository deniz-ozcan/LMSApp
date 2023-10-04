namespace lmsapp.entity
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
    }
}