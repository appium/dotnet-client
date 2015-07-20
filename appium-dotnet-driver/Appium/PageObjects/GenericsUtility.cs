using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    internal class GenericsUtility
    {
        public static bool MatchGenerics(Type generalType, List<Type> possibleParameters, Type targetType)
        {
            foreach (var type in possibleParameters)
            {
                Type fullType = generalType.MakeGenericType(type);
                if (fullType.Equals(targetType))
                    return true;
            }

            return false;
        }

        public static object CraeteInstanceOfSomeGeneric(Type generalType, Type genericParameter, 
            Type[] argTypes, object[] argValues)
        {
            Type generic = generalType.MakeGenericType(genericParameter);
            return generic.GetConstructor(argTypes).Invoke(argValues);
        }
    }
}
