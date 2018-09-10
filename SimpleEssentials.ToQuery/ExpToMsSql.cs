using System;
using System.Linq.Expressions;
using SimpleEssentials.ToQuery.Expression;
using SimpleEssentials.ToQuery.Expression.Interpretor;
using SimpleEssentials.ToQuery.Reflector;

namespace SimpleEssentials.ToQuery
{
    public class ExpToMsSql : IExpToQuery
    {
        private readonly IReflector _reflector = new MsSqlReflector();
        private readonly IInterpreter _interpreter = new MsSqlInterpreter();
        private readonly ICustomCommand _command = new CustomCommand();
        private IQueryObject _wherePart = new QueryObject();

        public IExpToQuery Where<T>(Expression<Func<T, bool>> expression = null)
        {
            if (expression != null)
            {
                _wherePart = ExpressionToQuery.Convert(expression, _interpreter);
                _command.Concat($" where {_wherePart.Query}");
            }

            if (_wherePart == null)
                return null;

            _wherePart.Query = _command.GetCommand();
            _wherePart.Parameters = _wherePart.Parameters;
            return this;
        }

        public IExpToQuery Select<T>()
        {
            var type = typeof(T);
            _command.Concat(
                $"select * from {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]} {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]}");
            return this;
        }

        public IExpToQuery InnerJoinOn<T, T2>(Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            _command.Concat(
                $"inner join {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]} {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]}");
            _command.Concat($"on {ExpressionToQuery.Convert(expression, _interpreter).Query}");
            return this;
        }

        public IExpToQuery LeftJoinOn<T, T2>(Expression<Func<T, T2, bool>> expression)
        {
            var type = typeof(T2);
            _command.Concat(
                $"left join {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]} {_interpreter.DelimitedCharacters[0]}{_reflector.GetTableName(type, type)}{_interpreter.DelimitedCharacters[1]}");
            _command.Concat($"on {ExpressionToQuery.Convert(expression, _interpreter).Query}");
            return this;
        }

        public IExpToQuery On<T, T2>( Expression<Func<T, T2, bool>> expression)
        {
            _command.Concat($"on {ExpressionToQuery.Convert(expression, _interpreter).Query}");
            return this;
        }

        public IExpToQuery Limit(int limitCount)
        {
            _command.Concat($"LIMIT {limitCount}");
            return this;
        }

        public IQueryObject Generate()
        {
            return this._wherePart;
        }

        
    }
}
