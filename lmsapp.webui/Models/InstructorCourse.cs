
using lmsapp.entity;
using lmsapp.webui.Identity;

namespace lmsapp.webui.Models
{
    public class InstructorCourse
    {
        public User Instructor { get; set; }
        public Course Course { get; set; }
    }
}