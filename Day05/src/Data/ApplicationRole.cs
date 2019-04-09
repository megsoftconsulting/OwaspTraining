using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Day05.Data
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}