using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class Bookmarks
    {
        public int CodeBookmarks { get; set; }
        public int UnicCodeUsers { get; set; }
        public int CodeTitle { get; set; }

        public virtual Title CodeTitleNavigation { get; set; }
        public virtual Users UnicCodeUsersNavigation { get; set; }
    }
}
