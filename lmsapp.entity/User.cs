using Microsoft.AspNetCore.Identity;

namespace lmsapp.entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}