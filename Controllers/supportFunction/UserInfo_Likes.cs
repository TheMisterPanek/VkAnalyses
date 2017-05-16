using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.supportFunction
{
    public class UserInfo_Likes
    {
        public string UserName { get; set; }
        public long Uid { get; set; }
        public long Count { get; set; }

        public UserInfo_Likes(string userName,long uid,long count = 0)
        {
            this.UserName = userName;
            this.Uid = uid;
            this.Count = count;
        }

        public static UserInfo_Likes operator++(UserInfo_Likes value)
        {
            value.Count++;
            return value;
        }

        public override string ToString()
        {
            return $"{UserName}";
        }
    }
}