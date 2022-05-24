using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Kursovoi
{
    public partial class Title
    {
        public Title()
        {
            Bookmarks = new HashSet<Bookmarks>();
        }

        public int CodeTitle { get; set; }
        public string NameOfTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CodeCodeTypeOfComics { get; set; }
        public string Publisher { get; set; }
        public int CodeAuthor { get; set; }
        public string Genre { get; set; }
        public int CodeTranslator { get; set; }
        public int CodeTranslation { get; set; }
        public int CodeDescription { get; set; }
        public string Photo { get; set; }
        public string Link { get; set; }
        public int CodePhChepter { get; set; }

        public virtual Author CodeAuthorNavigation { get; set; }
        public virtual TypeOfComics CodeCodeTypeOfComicsNavigation { get; set; }
        public virtual Description CodeDescriptionNavigation { get; set; }
        public virtual Photochepter CodePhChepterNavigation { get; set; }
        public virtual Translation CodeTranslationNavigation { get; set; }
        public virtual Translator CodeTranslatorNavigation { get; set; }
        public virtual ICollection<Bookmarks> Bookmarks { get; set; }
    }
}
