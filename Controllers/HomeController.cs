using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VK_Analyze.Controllers.functions;
using VK_Analyze.Models;

using VkNet;
using VkNet.Utils;

namespace VK_Analyze.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }


        public ActionResult Index()
        {
            if (Session["VkApi"] == null)
            {
                SessionWorker.UpdateSession(new CookieTokenWorker(Request.Cookies["VkAnalyses"]), Session);
                ViewBag.User = VkAccount.GetAccountInfo((VkApi)Session["VkApi"]);
            }
            return View();
        }



        [HttpGet]
        public ActionResult Friends()
        {
            VkApi vk = (VkApi)Session["VkApi"];
            UserFriendsView userChoise = new UserFriendsView();
            userChoise.ScreenName = VkAccount.GetAccountInfo(vk).ScreenName;
            return Friends(userChoise);
        }

        [HttpPost]
        public ActionResult Friends(UserFriendsView userChoise)
        {

            VkApi vk = (VkApi)Session["VkApi"];
            long userID = VkAccount.GetAccountInfo(vk, userChoise.ScreenName).Id;
            Dictionary<string, int> Dict = VkFriends.GetDictionaryFriendsGroupByCity(vk, userID);
            ViewBag.Citys = supportFunction.Converter.ToCityInfoCollection(Dict, supportFunction.IgnoreSingle.Ignore).ToList();
            List<KeyValuePair<string, int>> listCitys = (from x in Dict where x.Value > 1 select x).ToList();

            string js_citys = supportFunction.Converter.ToJSArray("Город", "Люди", listCitys);
            string js_data = $@"var data = google.visualization.arrayToDataTable({js_citys});";
            ViewBag.js_arrayFriends = js_data;

            return View();
        }

        private void WriteCitysToViewBag(VkApi vk, long userID)
        {

        }




        public ActionResult About()
        {

            ViewBag.Message = "Страница информации";
            return View();
        }

        public ActionResult Contact()
        {

            ViewBag.Message = "Контактная информация";
            return View();
        }
    }
}