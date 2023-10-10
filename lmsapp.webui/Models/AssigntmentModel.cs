
using lmsapp.entity;

namespace lmsapp.webui.Models
{
    public class AssigntmentModel
    {
        public Assignment Assignment { get; set; }
        public bool IsSubmitted { get; set; }
        public int AssigneeId { get; set; }
    }
}