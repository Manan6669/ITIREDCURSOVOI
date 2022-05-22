using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class Translator
    {
        public Translator()
        {
            Title = new HashSet<Title>();
        }

        public int CodeTranslator { get; set; }
        public string Translator1 { get; set; }
        public string PhotoTranslator { get; set; }

        public virtual ICollection<Title> Title { get; set; }
    }
}
