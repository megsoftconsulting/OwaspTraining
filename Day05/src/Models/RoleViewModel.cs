using System.Collections.Generic;

namespace Day05.Models
{
    public class RoleViewModel
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

    public class UserRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<RoleViewModel> RoleViewModels { get; set; }
    }
}