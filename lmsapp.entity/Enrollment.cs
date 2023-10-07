
namespace lmsapp.entity
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public User Student { get; set; }
        public string StudentId { get; set; }
    }
}