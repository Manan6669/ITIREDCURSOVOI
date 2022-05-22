using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class Users
    {
        public Users()
        {
            Bookmarks = new HashSet<Bookmarks>();
        }

        public int UnicCodeUsers { get; set; }
        public string PhotoUsers { get; set; }
        public string UsersPassword { get; set; }
        public string UsersLoqin { get; set; }
        public int UsersRole { get; set; }

        public virtual ICollection<Bookmarks> Bookmarks { get; set; }
    }
}
