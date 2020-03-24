using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebsite.Models;
using PagedList;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace PersonalWebsite.Controllers
{
    public class HomeController : Controller
    {
        //注册实体集
        private AquariumEntities2 db = new AquariumEntities2();
        private AquariumEntities3 db3 = new AquariumEntities3();
        private AquariumEntities4 db4 = new AquariumEntities4();
        private AquariumEntities5 db5 = new AquariumEntities5();
        private AquariumEntities7 db7 = new AquariumEntities7();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Readme()
        {
            return View();
        }

        public ActionResult News(string commandName, int? page)
        {
            try
            {
                System.Linq.IQueryable<PersonalWebsite.Models.News> news;

                int year = 0, month = 0;
                string type = null;
                if (Request["year"] != null && Request["year"] != "")
                    year = Convert.ToInt32(Request["year"]);
                if (Request["yearMonth"] != null && Request["yearMonth"] != "")
                {
                    year = Convert.ToInt32(Request["yearMonth"].Split('-')[0]);
                    month = Convert.ToInt32(Request["yearMonth"].Split('-')[1]);
                }
                if (Request["type"] != null && Request["type"] != "")
                    type = Request["type"];

                //进行查询
                if (year != 0 && month == 0)
                {
                    //使用LINQ进行查询
                    news = from n in db.News
                           where n.NewsDate.Year == year
                           select n;
                }
                else if (year != 0 && month != 0)
                {
                    //使用LINQ进行查询
                    news = from n in db.News
                           where n.NewsDate.Year == year && n.NewsDate.Month == month
                           select n;
                }
                else if (type != null)
                {
                    //使用LINQ进行查询
                    news = from n in db.News
                           where n.NewsType == type
                           select n;
                }
                else
                {
                    news = from n in db.News
                           select n;
                }

                //进行分页
                news = news.OrderByDescending(n => n.NewsID);
                const int pageItems = 10;
                int currentPage = (page ?? 1);
                IPagedList<News> pageNews = news.ToPagedList(currentPage, pageItems);
                ViewNews vnews = new ViewNews();
                vnews.NewsList = pageNews;
                vnews.year = year;
                vnews.month = month;
                vnews.type = type;

                return View("News", vnews);
            }
            catch (Exception ex)
            {
                return Content("<script>alert('Wrong arguments!');location.href='/Home/News';</script>");
            }
        }

        public new ActionResult Profile()
        {
            return View(db3.Profile.FirstOrDefault());
        }

        /// <summary>
        /// 生成不同随机数的方法
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="count">个数</param>
        /// <returns></returns>
        private static int[] GetRandom(int min, int max, int count)
        {
            int[] maxArray = new int[max];
            for(int i=0; i<max; i++)
            {
                maxArray[i] = min + i;
            }
            int[] rArray = new int[count];
            Random rd = new Random();
            int tmp = max;
            for(int i=0; i<count; i++)
            {
                int tIndex = rd.Next(0, tmp);
                rArray[i] = maxArray[tIndex];
                maxArray[tIndex] = maxArray[--tmp];
            }
            return rArray;
        }

        public ActionResult Post(int? page)
        {
            //是否需要回答问题后才能查看内容
            string PostQuestion = ConfigurationManager.AppSettings["PostQuestion"];
            ViewBag.PostQuestion = PostQuestion;

            //若是管理员登录，则默认不需要回答问题
            if (Session["username"] != null)
            {
                ViewBag.PostQuestion = "false";
            }
            
            //若需要回答问题
            if (PostQuestion == "true")
            {
                string jsonFile = Server.MapPath("~/Files/") + "questions.json";
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader file = System.IO.File.OpenText(jsonFile);
                JsonTextReader reader = new JsonTextReader(file);
                JObject obj = (JObject)JToken.ReadFrom(reader);
                file.Close(); //防止占用文件
                var list = obj["questions"].ToString();
                JArray ja = (JArray)JsonConvert.DeserializeObject(list);
                var selected = GetRandom(0, ja.Count, 3);
                List<ViewQuestion> vqlist = new List<ViewQuestion>();
                foreach (var num in selected)
                {
                    ViewQuestion vq = new ViewQuestion();
                    vq.Content = ja[num]["content"].ToString();
                    vq.Answer = ja[num]["answer"].ToString();
                    vqlist.Add(vq);
                }
                ViewBag.vqlist = vqlist;
            }

            System.Linq.IQueryable<PersonalWebsite.Models.Post> post;
            //进行查询
            post = from p in db4.Post
                   select p;
            //分页显示
            post = post.OrderByDescending(p => p.PostID);
            const int pageItems = 10;
            int currentPage = (page ?? 1);
            IPagedList<Post> pagePost = post.ToPagedList(currentPage, pageItems);
            ViewPost vpost = new ViewPost();
            vpost.PostList = pagePost;

            return View(vpost);
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db5.Contact.Add(contact);
                    db5.SaveChanges();
                }
                return Content("<script>alert('Operation succeeded!');location.href='/Home/Contact';</script>");
            }
            catch (Exception e)
            {
                return Content("<script>alert('Error occurred when updating the database.';location.href='/Home/Contact';</script>");
            }
        }

        //文章列表
        public ActionResult Article(string commandName, int? page)
        {
            try
            {
                System.Linq.IQueryable<PersonalWebsite.Models.Article> article;

                int year = 0, month = 0;
                string type = null;
                if (Request["year"] != null && Request["year"] != "")
                    year = Convert.ToInt32(Request["year"]);
                if (Request["yearMonth"] != null && Request["yearMonth"] != "")
                {
                    year = Convert.ToInt32(Request["yearMonth"].Split('-')[0]);
                    month = Convert.ToInt32(Request["yearMonth"].Split('-')[1]);
                }
                if (Request["type"] != null && Request["type"] != "")
                    type = Request["type"];

                //进行查询
                if (year != 0 && month == 0)
                {
                    //使用LINQ进行查询
                    article = from n in db7.Article
                           where n.ArticleDate.Year == year
                           select n;
                }
                else if (year != 0 && month != 0)
                {
                    //使用LINQ进行查询
                    article = from n in db7.Article
                           where n.ArticleDate.Year == year && n.ArticleDate.Month == month
                           select n;
                }
                else if (type != null)
                {
                    //使用LINQ进行查询
                    article = from n in db7.Article
                           where n.ArticleType == type
                           select n;
                }
                else
                {
                    article = from n in db7.Article
                           select n;
                }

                //进行分页
                article = article.OrderByDescending(n => n.ArticleID);
                const int pageItems = 10;
                int currentPage = (page ?? 1);
                IPagedList<Article> pageArticle = article.ToPagedList(currentPage, pageItems);
                ViewArticle varticle = new ViewArticle();
                varticle.ArticleList = pageArticle;
                varticle.year = year;
                varticle.month = month;
                varticle.type = type;

                return View("Article", varticle);
            }
            catch (Exception ex)
            {
                return Content("<script>alert('Wrong arguments!');location.href='/Home/Article';</script>");
            }
        }

        //写新文章
        public ActionResult Create()
        {
            //此功能需先进行管理员登录才可用
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = Request["title"];
                    string titleStr = title + "猋"; //用生僻字当作标签，防止标题显示出错
                    string articleContent = HttpUtility.UrlDecode(Request["hiddenContent"]);
                    string Markdown = HttpUtility.UrlDecode(Request["hiddenContent1"]);
                    article.ArticleDate = DateTime.Now;
                    article.ArticleContent = articleContent;
                    article.ArticleContent = article.ArticleContent.Insert(0, titleStr);
                    article.Markdown = Markdown;
                    db7.Article.Add(article);
                    db7.SaveChanges();
                }
                return Content("<script>alert('Operation succeeded!');location.href='/Home/Create';</script>");
            }
            catch (Exception e)
            {
                return Content("<script>alert('Error occurred when updating the database.');location.href='/Home/Create';</script>");
            }
        }

        //编辑文章
        public ActionResult Edit(int id = 0)
        {
            //此功能需先进行管理员登录才可用
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Article article = db7.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //暂时显式指定全部的参数
                    string title = Request["title"];
                    string ID = Request["hiddenID"];
                    string date = Request["hiddenDate"];
                    string titleStr = title + "猋"; //用生僻字当作标签，防止标题显示出错
                    string articleContent = HttpUtility.UrlDecode(Request["hiddenContent"]);
                    string Markdown = HttpUtility.UrlDecode(Request["hiddenContent1"]);
                    article.ArticleDate = Convert.ToDateTime(date);
                    article.ArticleID = Convert.ToInt32(ID);
                    article.ArticleContent = articleContent;
                    article.ArticleContent = article.ArticleContent.Insert(0, titleStr);
                    article.Markdown = Markdown;
                    db7.Entry(article).State = EntityState.Modified;
                    db7.SaveChanges();
                }
                return Content("<script>alert('Operation succeeded!');location.href='/Home/Article';</script>");
            }
            catch (Exception e)
            {
                return Content("<script>alert('Error occurred when updating the database.');location.href='/Home/Article';</script>");
            }
        }

        //显示文章
        public ActionResult Content(int id, string title)
        {
            ViewData["title"] = title;
            ViewData["id"] = id;
            Article article = db7.Article.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
