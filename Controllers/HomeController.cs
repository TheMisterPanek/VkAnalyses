using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VK_Analyze.Controllers.functions;
using VK_Analyze.Models;
using VK_Analyze.Models.EntityFramework;
using VkNet;
using VkNet.Utils;

namespace VK_Analyze.Controllers
{
    public class HomeController : Controller
    {
         VkApi vk;

        VkNet.Model.User userInfo;

        public HomeController()
        {
            CookieTokenWorker cookie = new CookieTokenWorker(new HttpCookie("VkAnalyses"));
        }


        public ActionResult Index()
        {
           
            return View();
        }

        public void Friends(params string[] args)
        {
           
        }

        [HttpGet]
        public ActionResult Friends(string userID = "0")
        {
            ViewBag.Text = "Друзья";
            ViewBag.Users = VkFriends.GetUsers(vk);
            return View();
        }



        [HttpPost]
        public ActionResult Friends(UserFriendsView model)
        {
            ViewBag.User = userInfo;
            if (ModelState.IsValid)
            {
                try
                {
                    CookieTokenWorker cookie = new CookieTokenWorker(new HttpCookie("VkAnalyses"));
                    VkApi vkApi = cookie.GetVkApiFromCookie();
                    ViewBag.Users = VkFriends.GetUsers(vkApi, model.UserID);
                }
                catch(Exception)
                {
                    ViewBag.Users = new User[0];
                }
            }
            return View();
        }


        public ActionResult About()
        {
            ViewBag.User = userInfo;
            ViewBag.Message = "Страница информации";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.User = userInfo;
            ViewBag.Message = "Контактная информация";
            return View();
        }
    }
}