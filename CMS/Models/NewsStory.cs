using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models
{
    public class NewsStory
    {
        public int NewsStoryID { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<KeyWord> KeyWords { get; set; }
    }
}