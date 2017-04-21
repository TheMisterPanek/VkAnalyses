using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.functions
{
    public sealed class CookieTokenWorker
    {
        HttpCookie cookies;

        public CookieTokenWorker(HttpCookie cookie)
        {
            cookies = cookie;
        }

        public CookieTokenWorker(string cookieName)
        {
            cookies = new HttpCookie(cookieName);
        }

        public bool isContainsToken()
        {
            return cookies["token"] != null;
        }

        public VkNet.VkApi GetVkApiFromCookie()
        {
            VkNet.VkApi vk = null;
            if (cookies != null)
            {
                if (isContainsToken())
                {
                    vk = VkLogin.GetInstance(cookies["token"]);
                }
            }
            return vk;
        }

        internal void UpdateInform()
        {
            throw new NotImplementedException();
        }
    }
}