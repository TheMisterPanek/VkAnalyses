using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using VkNet;
using VkNet.Enums.Filters;

namespace VK_Analyze.Controllers.functions
{
    public class VkLogin
    {

        public const int APPLICATION_ID = 5910704;

        public static bool IsAuthorized(VkApi userInfo)
        {
            if (userInfo != null)
            {
                if(userInfo.IsAuthorized)
                {
                    return isValidToken(userInfo.Token);
                }
            }
            return false;
        }



        public static VkApi GetInstance(string token)
        {
            VkApi userInfo = new VkApi();
            userInfo.Authorize(token);
            return userInfo;
        }

        public static bool isValidToken(string token)
        {
            using (VkApi vk = new VkApi())
            {
                try
                {
                    vk.Authorize(token);
                    //Для провоцирования ошибки
                    long uid =  vk.Users.Get(vk.Account.GetProfileInfo().ScreenName, VkNet.Enums.Filters.ProfileFields.All).Id;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
         
        }


       
    }
}