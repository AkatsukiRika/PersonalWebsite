using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace PersonalWebsite.Models
{
    public class ViewNews
    {
        public IPagedList<News> NewsList { get; set; }
        public int year = default(int);
        public int month = default(int);
        public string type = default(string);
    }
}