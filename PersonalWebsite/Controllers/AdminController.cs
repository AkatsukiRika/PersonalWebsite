using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace PersonalWebsite.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return Content("<script>location.href='/Admin/Management';</script>");
            }
            return View();
        }

        public ActionResult Management()
        {
            //已登录则直接显示页面
            if (Session["username"] != null)
            {
                return View();
            }

            try
            {
                //登录后进行验证，使管理员可以访问后台管理界面
                string username = Request["username"].ToString();
                string password = Request["password"].ToString();

                //硬编码用户名以及密码
                if (username.Equals("rika") && password.Equals("960207"))
                {
                    Session["username"] = "rika";
                    return View();
                }
                else
                {
                    return Content("<script>alert('ログインに失敗しました。');location.href='/Admin';</script>");
                }
            }
            catch (Exception e)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string filePath = Server.MapPath("~/Files/") + file.FileName;
                file.SaveAs(filePath);

                var json = new List<Object>();
                var data = new List<object>();
                data.Add(new
                {
                    src = file.FileName
                });
                json.Add(new
                {
                    code = 0,
                    msg = "Upload succeeded",
                    data
                });
                return Json(data);
            }
            else
            {
                return Json(new
                {
                    code = 1,
                    msg = "Upload failed"
                });
            }
        }

        //文件表格
        [HttpPost]
        public ActionResult FileTable(int page, int limit)
        {
            DirectoryInfo folder = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Files\\");
            var json = new List<Object>();
            var data = new List<Object>();
            int count = 0;
            foreach(FileInfo file in folder.GetFiles())
            {
                count++;
                string fileName = file.Name;
                string fileType = Path.GetExtension(file.FullName);
                string fileSize = file.Length.ToString();
                string createTime = file.CreationTime.ToString();
                data.Add(new
                {
                    filename = fileName,
                    type = fileType,
                    size = fileSize,
                    create_time = createTime
                });
            }
            json.Add(new
            {
                code = 0,
                msg = "Load succeeded",
                count,
                data
            });
            return Json(data);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Content("<script>alert('ログアウトに成功しました。');location.href='/Admin';</script>");
        }
    }
}
