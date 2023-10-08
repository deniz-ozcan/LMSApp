
namespace lmsapp.entity
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public bool IsSubmitted { get; set; }
    }
}