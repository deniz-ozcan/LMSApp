using Microsoft.AspNetCore.Identity;
using lmsapp.entity;
namespace lmsapp.webui.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}