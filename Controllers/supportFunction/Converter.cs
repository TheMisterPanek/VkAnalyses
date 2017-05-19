using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.supportFunction
{
    public static class Converter
    {
        public static IEnumerable<CityInfo> ToCityInfoCollection(Dictionary<string,int> citys, IgnoreSingle ignoreSingle = IgnoreSingle.None)
        {
            List<CityInfo> cityInfoCollection = new List<CityInfo>();

            foreach (string key in citys.Keys)
            {
                if(ignoreSingle==IgnoreSingle.Ignore)
                {
                    if(citys[key]<=1)
                    {
                        continue;
                    }
                }
                cityInfoCollection.Add(new CityInfo(key, citys[key]));
            }

            return cityInfoCollection;
        }

        public static string ToJSArray(IEnumerable<KeyValuePair<string, int>> value)
        {
            return ToJSArray("","",value);
        }


        public static string ToJSArray(string firstColumnName, string secondColumnName, IEnumerable<KeyValuePair<string,int>> value)
        {
            string text = "";
            text += "[";
            if(!string.IsNullOrEmpty(firstColumnName) && !string.IsNullOrEmpty(secondColumnName))
            {
                text += $"['{firstColumnName}','{secondColumnName}'],";
            }
            foreach (KeyValuePair<string,int> item in value)
            {
                text += $"['{item.Key}',{item.Value}],";    
            }
            text = text.Remove(text.Length-1, 1);
            text += "]";
            return text;
        }


    }



}