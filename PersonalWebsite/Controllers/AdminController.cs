using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult FileUpload(HttpPostedFileBase file)
        {
            if(file != null)
            {
                string filePath = Server.MapPath("~/Files/") + file.FileName;
                file.SaveAs(filePath);
                return Content("<script>alert('アップロードに成功しました');location.href='/Admin/FileUpload';</script>");
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Content("<script>alert('ログアウトに成功しました。');location.href='/Admin';</script>");
        }
    }
}
