using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS.Models
{
    public class KeyWord
    {
        public int KeyWordID { get; set; }

        public string keyWordContent { get; set; }

        public virtual ICollection<NewsStory> NewsStories { get; set; }
    }
}