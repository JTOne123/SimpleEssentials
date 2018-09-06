using System;
using System.Collections.Generic;
using FastMember;

namespace SimpleEssentials.LinqToDb.Reflector
{
    public interface IReflector
    {
        string GenerateCreateColumns(object obj, Type overrideType = null);
        string GenerateInsertColumnNames(object obj, Type overrideType = null);
        string GetTableName(object obj, Type overrideType = null);
        string GetKeyName(object obj, string propNameOverride = null);
        bool IsIdentityMember(Member member);
        List<Member> GetNonIdentityProperties(object obj, Type overrideType = null);
        Member GetIdentityProperty(object obj, Type overrideType = null);
        bool IsNullable(Type type);
        string TypeToSqlType(Type type);
    }
}
