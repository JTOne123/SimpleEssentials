using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper.Contrib.Extensions;
using FastMember;

namespace SimpleEssentials.ToQuery.Reflector
{
    public class MySqlReflector : IReflector
    {
        public string GenerateCreateColumns(object obj, Type overrideType = null)
        {
            var nonIdentityProps = GetNonIdentityProperties(obj);
            var identityProp = GetIdentityProperty(obj);
            var createSql = "(";

            if (identityProp != null)
            {
                if (identityProp.IsDefined(typeof(ExplicitKeyAttribute)))
                    createSql += "`" + identityProp.Name + "` " + TypeToSqlType(identityProp.Type) + " primary key, ";
                else
                    createSql += "`" + identityProp.Name + "` " + TypeToSqlType(identityProp.Type) + " identity(1,1) primary key, ";

            }

            for (var i = 0; i < nonIdentityProps.Count; i++)
            {
                createSql += "`" + nonIdentityProps[i].Name + "` " + TypeToSqlType(nonIdentityProps[i].Type);
                if (IsNullable(nonIdentityProps[i].Type))
                    createSql += " NULL";
                else
                    createSql += " NOT NULL";

                if (i != nonIdentityProps.Count - 1)
                    createSql += ", ";
            }

            createSql += ")";
            return createSql;
        }
        public string GenerateInsertColumnNames(object obj, Type overrideType = null)
        {
            var nonIdProps = GetNonIdentityProperties(obj, overrideType);
            var finalString = "(";
            for (var i = 0; i < nonIdProps.Count; i++)
            {
                finalString += nonIdProps[i].Name;
                if (i != nonIdProps.Count - 1)
                    finalString += ", ";
            }
            finalString += ") values (";
            for (var i = 0; i < nonIdProps.Count; i++)
            {
                finalString += "@" + nonIdProps[i].Name;
                if (i != nonIdProps.Count - 1)
                    finalString += ", ";
            }
            finalString += ")";
            return finalString;
        }

        public string GetTableName(object obj, Type overrideType = null)
        {
            var type = overrideType ?? obj.GetType();
            string name;
            var tableAttr = type
                .GetTypeInfo();

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

        public string GetKeyName(object obj, string propNameOverride = null)
        {
            var accesor = TypeAccessor.Create(obj.GetType());
            var propWithKeyAttribute = accesor.GetMembers().FirstOrDefault(x => x.IsDefined(typeof(KeyAttribute)) || x.IsDefined(typeof(ExplicitKeyAttribute)));
            if (propWithKeyAttribute != null) return propWithKeyAttribute.Name;

            if (string.IsNullOrEmpty(propNameOverride)) return null;

            var memeber = accesor.GetMembers().FirstOrDefault(x => x.Name == propNameOverride);
            return memeber?.Name;
        }

        public bool IsIdentityMember(Member member)
        {
            if (member.IsDefined(typeof(KeyAttribute)) ||
                member.IsDefined(typeof(ExplicitKeyAttribute)) ||
                member.IsDefined(typeof(ComputedAttribute)))
                return true;

            return member.Name.ToLower() == "id";
        }

        public List<Member> GetNonIdentityProperties(object obj, Type overrideType = null)
        {
            var type = overrideType ?? obj.GetType();
            var accessor = TypeAccessor.Create(type, true);

            return accessor.GetMembers().Where(member => !IsIdentityMember(member)).ToList();
        }

        public Member GetIdentityProperty(object obj, Type overrideType = null)
        {
            var type = overrideType ?? obj.GetType();
            var accessor = TypeAccessor.Create(type, true);
            var tmpMembers = accessor.GetMembers().Where(x =>
                x.IsDefined(typeof(KeyAttribute)) || x.IsDefined(typeof(ExplicitKeyAttribute))).ToList();
            if (tmpMembers.Any())
                return tmpMembers.First();

            tmpMembers = accessor.GetMembers().Where(x => x.Name.ToLower() == "id").ToList();
            return tmpMembers.Any() ? tmpMembers.First() : null;
        }

        public bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        public string TypeToSqlType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            if (type == typeof(string) || type == typeof(object))
                return ($"VARCHAR (MAX)");

            if (type == typeof(int) || type == typeof(Enum) || type == typeof(short))
                return ($"INT");

            if (type == typeof(long))
                return ($"BIGINT");

            if (type == typeof(DateTime))
                return ($"DATETIME");

            if (type == typeof(bool))
                return ($"BIT");

            if (type == typeof(double))
                return ($"FLOAT");

            if (type == typeof(decimal))
                return ($"DECIMAL (18,2)");

            if (type == typeof(byte))
                return ($"TINYINT");

            if (type == typeof(byte[]))
                return ($"VARBINARY (MAX)");

            if (type == typeof(Guid))
                return ($"uniqueidentifier");

            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (type == typeof(char))
                return ($"CHARACTER");

            return ($"VARCHAR (MAX)");
        }
    }
}
