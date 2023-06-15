using System;
using System.Collections.Generic;

#nullable disable

namespace WebBlogCore5.Models
{
    public partial class Category
    {
        public Category()
        {
            Posts = new HashSet<Post>();
        }

        public int CateId { get; set; }
        public string CateName { get; set; }
        public string Tittle { get; set; }
        public string Alias { get; set; }
        public string MetaDes { get; set; }
        public string MetaKey { get; set; }
        public string Thumb { get; set; }
        public bool Published { get; set; }
        public int? Ordering { get; set; }
        public int? Parents { get; set; }
        public int? Levels { get; set; }
        public string Icon { get; set; }
        public string Cover { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
