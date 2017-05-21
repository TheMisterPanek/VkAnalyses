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

        const string UNKNOW_CITY = "не известно";
           
        public static VkNet.Model.User[] GetUsers(VkApi vk, long userId)
        {
               
            VkNet.Model.RequestParams.FriendsGetParams categories = new VkNet.Model.RequestParams.FriendsGetParams();
            categories.Fields = VkNet.Enums.Filters.ProfileFields.All;
            categories.Count = 100;
            categories.UserId = userId;
            VkCollection < VkNet.Model.User > userList =
                vk.Friends.Get(categories);
            return userList.ToArray();
        }

        public static Dictionary<string,int> GetDictionaryFriendsGroupByCity(VkApi vk, long userID,int countFriends = -1)
        {
            VkNet.Model.User[] friends = GetUsers(vk,userID);
            Dictionary<string, int> cityDictionary = new Dictionary<string, int>();

            cityDictionary.Add(UNKNOW_CITY, 0);

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
                else
                {
                    cityDictionary[UNKNOW_CITY]++;
                }
            }
            return cityDictionary;
        }
    }
}