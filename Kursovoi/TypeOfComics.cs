using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class TypeOfComics
    {
        public TypeOfComics()
        {
            Title = new HashSet<Title>();
        }

        public int CodeTypeOfComics { get; set; }
        public string TypeOfComics1 { get; set; }

        public virtual ICollection<Title> Title { get; set; }
    }
}
