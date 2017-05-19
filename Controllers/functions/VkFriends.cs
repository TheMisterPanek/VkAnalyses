using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VK_Analyze.Controllers.supportFunction;
using VkNet;
using VkNet.Utils;

namespace VK_Analyze.Controllers.functions
{

    public class VkFriends
    {
        
        public static VkNet.Model.User[] GetUsers(VkApi vk, long userId)
        {
               
            VkNet.Model.RequestParams.FriendsGetParams categories = new VkNet.Model.RequestParams.FriendsGetParams();
            categories.Fields = VkNet.Enums.Filters.ProfileFields.All;
            categories.UserId = userId;
            VkCollection < VkNet.Model.User > userList =
                vk.Friends.Get(categories);
            return userList.ToArray();
        }

        public static Dictionary<string,int> GetDictionaryFriendsGroupByCity(VkApi vk, long userID)
        {
            VkNet.Model.User[] friends = GetUsers(vk,userID);
            Dictionary<string, int> cityDictionary = new Dictionary<string, int>();
            foreach (VkNet.Model.User item in friends)
            {
                if (item.City != null)
                {
                    if (!cityDictionary.ContainsKey(item.City.Title))
                    {
                        cityDictionary.Add(item.City.Title, 0);
                    }
                    cityDictionary[item.City.Title]++;
                }
            }
            return cityDictionary;
        }
    }
}