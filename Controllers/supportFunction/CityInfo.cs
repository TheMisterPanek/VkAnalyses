using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.supportFunction
{
    public class CityInfo
    {
        public string City { get; set; }
        public int CountFriends { get; set; }
        public CityInfo(string City,int CountFriends)
        {
            this.City = City;
            this.CountFriends = CountFriends;
        }
    }
}