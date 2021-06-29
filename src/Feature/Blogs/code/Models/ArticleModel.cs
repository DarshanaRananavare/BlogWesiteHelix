using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogWebsiteHelix.Feaure.Blogs.Models
{
    public class ArticleModel 
    {
        public virtual string Id { get; set; }
        public virtual string ItemName { get; set; }
        public virtual string Title { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string LongDescription { get; set; }
        public virtual string ArticleSmallImage { get; set; }
        public virtual string ArticleLargeImage { get; set; }
        public virtual DateTime PostedDate { get; set; }
        public virtual string Category { get; set; }
        public virtual List<string> Tags { get; set; }
        public virtual string Author { get; set; }
    }
}