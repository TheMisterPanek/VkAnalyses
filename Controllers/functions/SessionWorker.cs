using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VkNet;

namespace VK_Analyze.Controllers.functions
{
    public static class SessionWorker
    {
        public static void UpdateSession(CookieTokenWorker cookie,HttpSessionStateBase Session)
        {
            VkApi vk = (VkApi)Session["VkApi"];
            vk = cookie.GetVkApiFromCookie();
            if (vk == null)
            {
                vk = (VkApi)Session["VkApi"];
            }
            else
            {
                Session["VkApi"] = vk;
            }

        }
    }
}