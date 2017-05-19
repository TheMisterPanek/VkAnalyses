using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VK_Analyze.Controllers.functions;
using VK_Analyze.Models;
using VkNet;

namespace VK_Analyze.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
        }


        [AllowAnonymous]
        public ActionResult Login()
        {
            HttpCookie cookieReq = Request.Cookies["VkAnalyses"];
            CookieTokenWorker cookie = new CookieTokenWorker(cookieReq);
            //VkApi vk = cookie.GetVkApiFromCookie();
            //if (vk == null)
            //{
            //    vk = (VkApi)Session["VkApi"];
            //}
            //else
            //{
            //    Session["VkApi"] = vk;
            //}

            SessionWorker.UpdateSession(new CookieTokenWorker(cookieReq), Session);
            VkApi vk = (VkApi)Session["VkApi"];
            ViewBag.User = GetUserInform(vk);
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            Parser parser = new Parser(model.Token);
            string token = parser.ParseToken();

            if (!VkLogin.isValidToken(token))
            {
                ViewBag.Message = $"Ваш токен не является валидным";
                return View();
            }

            VkApi vk = new VkApi();

            HttpCookie cookie = new HttpCookie("VkAnalyses");
            cookie["token"] = token;
            Response.Cookies.Add(cookie);

            vk.Authorize(token);
            Session["VkApi"] = vk;
            ViewBag.User = GetUserInform(vk);
            return View();
        }

        private VkNet.Model.User GetUserInform(VkNet.VkApi vk, int id = 1)
        {
            VkNet.Model.User user = VkAccount.GetAccountInfo(vk);
            return user;
        }






        
    }
}