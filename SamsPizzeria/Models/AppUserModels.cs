using Microsoft.AspNetCore.Identity;

namespace SamsPizzeria.Models
{
    public class AppUser : IdentityUser
    {
        // no additional members are required
        // for basic Identity installation
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string PostTown { get; set; }
        public int Bonus { get; set; }
    }
}