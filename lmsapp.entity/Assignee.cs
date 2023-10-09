
namespace lmsapp.entity
{
    public class Assignee
    {
        public int AssigneeId { get; set; }
        public string UserId { get; set; }
        public int AssignmentId { get; set; }
        public bool isSubmitted { get; set; }
    }
}