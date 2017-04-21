using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VkNet;
using VkNet.Utils;

namespace VK_Analyze.Controllers.functions
{
    public class VkFriends
    {
        public static VkNet.Model.User[] GetUsers(VkApi vk,int userId = 0)
        {
            VkNet.Model.RequestParams.FriendsGetParams categories = new VkNet.Model.RequestParams.FriendsGetParams();
            categories.Fields = VkNet.Enums.Filters.ProfileFields.All;
            categories.UserId = userId;
            VkCollection < VkNet.Model.User > userList =
                vk.Friends.Get(categories);
            return userList.ToArray();
        }
    }
}