using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VkNet;

namespace VK_Analyze.Controllers.functions
{
    public class VkAccount
    {

        public static VkNet.Model.User GetAccountInfo(VkApi vk)
        {
            VkNet.Model.User user = null;
            if (VkLogin.IsAuthorized(vk))
            {
                user = vk.Users.Get(vk.Account.GetProfileInfo().ScreenName, VkNet.Enums.Filters.ProfileFields.All);

            }
            return user;
        }

        public static VkNet.Model.User GetAccountInfo(VkApi vk, string screenName)
        {
            VkNet.Model.User user = vk.Users.Get(screenName, VkNet.Enums.Filters.ProfileFields.All);
            return user;
        }

    }
}