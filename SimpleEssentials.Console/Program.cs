using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleEssentials.Console.Models;
using SimpleEssentials.LinqToDb;
using SimpleEssentials.LinqToDb.Expression.Visitors;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = DateTime.Now;
            var sql = LinqToSql.Select<CustomCampaign>().InnerJoinOn<CustomCampaign, TestItem>((x, y) => x.Id == y.Id).Where<CustomCampaign>(x => x.Id == 5 || x.Id == 0);
            //Expression<Func<CustomCampaign, bool>> exp = (x) => x.CreateDate == DateTime.Now;
           // Visitor.CreateFromExpression(exp).Visit<int>("", Test);

            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }

        static int Test()
        {
            return 0;
        }
    }
}
