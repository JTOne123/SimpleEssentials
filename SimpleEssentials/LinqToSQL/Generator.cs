using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using FastMember;
using SimpleEssentials.Extensions;

namespace SimpleEssentials.LinqToSQL
{
    public class Generator
    {
        public static string InsertAndReturnIdSql<T>(T obj)
        {
            return $"insert into {obj.GetTableName()}{GenerateInsertColumnNames(obj)} select cast(scope_identity() as int)";
        }

        public static string WhereSql<T>(T obj)
        {
            throw new NotImplementedException();
        }

        private static string GenerateInsertColumnNames(object obj)
        {
            var nonIdProps = obj.GetNonIdentityProperties();
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
    }
}
