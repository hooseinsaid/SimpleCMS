using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMS.Models
{
    public class Image
    {
        [Key,ForeignKey("NewsStory")]
        public int NewsStoryID { get; set; }
        

        public string ImageName { get; set; }
        public byte[] ImageContent { get; set; }

        
        public virtual NewsStory NewsStory { get; set; }
    }
}