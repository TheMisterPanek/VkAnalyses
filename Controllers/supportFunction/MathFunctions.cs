using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VK_Analyze.Controllers.supportFunction
{
    public static class MathFunctions
    {
        public static int Clamp(int value,int min,int max)
        {
            return Clamp<int>(value, min, max);
        }

        public static T Clamp<T>(T value,T min, T max)
        {
            dynamic tempValue = value;
            dynamic tempMin = min;
            dynamic tempMax = max;
            if (tempValue< tempMin)
            {
                value = min;
            }
            else if (tempValue > tempMax)
            {
                value = max;
            }
            return value;
        }
    }
}