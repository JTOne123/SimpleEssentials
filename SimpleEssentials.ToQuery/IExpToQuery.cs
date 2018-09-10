using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SimpleEssentials.ToQuery
{
    public interface IExpToQuery
    {
        IExpToQuery Where<T>(Expression<Func<T, bool>> expression = null);
        IExpToQuery Select<T>();
        IExpToQuery InnerJoinOn<T, T2>(Expression<Func<T, T2, bool>> expression);
        IExpToQuery LeftJoinOn<T, T2>(Expression<Func<T, T2, bool>> expression);
        IExpToQuery On<T, T2>(Expression<Func<T, T2, bool>> expression);
        IExpToQuery Limit(int limitCount);
        IQueryObject Generate();
    }
}
