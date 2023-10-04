using Microsoft.AspNetCore.Identity;
using lmsapp.webui.Identity;
using lmsapp.entity;

namespace lmsapp.webui.Models
{
    public class AdminPanelModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public IEnumerable<User> Users { get; set; }
        public List<Course> Courses { get; set; }
    }
}