using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.supportFunction
{
    public static class Converter
    {
        public static IEnumerable<CityInfo> ToCityInfoCollection(Dictionary<string,int> citys)
        {
            List<CityInfo> cityInfoCollection = new List<CityInfo>();

            foreach (string key in citys.Keys)
            {
                cityInfoCollection.Add(new CityInfo(key, citys[key]));
            }

            return cityInfoCollection;
        }
    }
}