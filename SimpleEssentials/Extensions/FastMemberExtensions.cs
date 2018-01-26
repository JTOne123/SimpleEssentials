using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;
using FastMember;

namespace SimpleEssentials.Extensions
{
    public static class FastMemberExtensions
    {
        public static string GetTableName(this object obj)
        {
            var type = obj.GetType();
            string name;
            var tableAttr = type
#if NETSTANDARD1_3
                .GetTypeInfo()
#endif
                .GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
            if (tableAttr != null)
            {
                name = tableAttr.Name;
            }
            else
            {
                name = type.Name + "s";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }
            return name;
        }

        public static string GetKeyName(this object obj, string propNameOverride = null)
        {
            var accesor = TypeAccessor.Create(obj.GetType());
            var propWithKeyAttribute = accesor.GetMembers().FirstOrDefault(x => x.IsDefined(typeof(KeyAttribute)) || x.IsDefined(typeof(ExplicitKeyAttribute)));
            if (propWithKeyAttribute != null) return propWithKeyAttribute.Name;

            if (string.IsNullOrEmpty(propNameOverride)) return null;

            var memeber = accesor.GetMembers().FirstOrDefault(x => x.Name == propNameOverride);
            return memeber?.Name;
        }

        public static bool IsIdentityMember(Member member)
        {
            if (member.IsDefined(typeof(KeyAttribute)) || member.IsDefined(typeof(ExplicitKeyAttribute)))
                return true;

            return member.Name.ToLower() == "id";
        }

        public static List<Member> GetNonIdentityProperties(this object obj)
        {
            var type = obj.GetType();

            var accessor = TypeAccessor.Create(type, true);

            var tmpMemebers = accessor.GetMembers().ToList();
            return tmpMemebers.Where(member => !IsIdentityMember(member)).ToList();
        }
    }
}
