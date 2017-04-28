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
            ViewBag.User = GetUserInform(cookie.GetVkApiFromCookie());
            return View();
        }



        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            HttpCookie cookieReq = Request.Cookies["VkAnalyses"];
            if (cookieReq == null || !VkLogin.isValidToken(cookieReq["token"]))
            {
                HttpCookie cookie = new HttpCookie("VkAnalyses");
                cookie["token"] = model.Token;
               //cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Set(cookie);
            }
            VkNet.VkApi vk = VkLogin.GetInstance(model.Token);
            ViewBag.User = GetUserInform(vk);
            return View();
        }

        private VkNet.Model.User GetUserInform(VkNet.VkApi vk,int id = 1)
        {
            VkNet.Model.User user = null;
            user = VkAccount.GetAccountInfo(vk);
            return user;
        }


        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Запрос перенаправления к внешнему поставщику входа
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        

    
    
    
   

       

        #region Вспомогательные приложения
     

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

         
        }
        #endregion
    }
}