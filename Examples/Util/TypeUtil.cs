using System.Collections.Generic;
using System.Linq;

namespace Examples.Util
{
    public class TypeUtil
    {
        public static Dictionary<string, object> GetDictionaryFromObject<T>(T obj)
        {
            return obj.GetType().GetProperties().ToDictionary(
                propertyInfo => propertyInfo.Name,
                propertyInfo => propertyInfo.GetValue(obj)
            );
        }
    }
}