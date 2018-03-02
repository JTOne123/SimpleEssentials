using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using FastMember;

namespace SimpleEssentials.Cache
{
    internal static class CacheHelper
    {
        public static bool IsKeyValid(string cacheKey)
        {
            return !string.IsNullOrEmpty(cacheKey);
        }

        public static string GetObjectFieldKey(object obj, string propNameOverride = null)
        {
            var accesor = TypeAccessor.Create(obj.GetType());
            var propWithKeyAttribute = accesor.GetMembers().FirstOrDefault(x => x.IsDefined(typeof(KeyAttribute)));
            if (propWithKeyAttribute == null)
            {
                if (string.IsNullOrEmpty(propNameOverride)) return null;

                var memeber = accesor.GetMembers().FirstOrDefault(x => x.Name == propNameOverride);
                return memeber == null ? null : accesor[obj, propNameOverride].ToString();
            }
            

            var propName = propWithKeyAttribute.Name;
            return accesor[obj, propName].ToString();

            //var type = obj?.GetType();
            //var properties = type?.GetProperties();
            //if (properties == null)
            //    return null;

            //return (from prop in properties
            //    where Attribute.IsDefined(prop, typeof(KeyAttribute))
            //    select prop.GetValue(obj).ToString()).FirstOrDefault();
        }

        public static DateTime GetExpirationDate(TimeSpan lifeSpan)
        {
            return DateTime.Now.Add(lifeSpan);
        }
    }
}
