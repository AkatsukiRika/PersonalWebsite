using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace PersonalWebsite.Models
{
    public class ViewPost
    {
        public IPagedList<Post> PostList { get; set; }
    }
}