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
using SimpleEssentials.Log;
using SimpleEssentials.Log.Writters;
using SimpleEssentials.ToQuery;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //test(2);
            var testVariable = 2;
            var queryObject = new ExpToMySql().Select<CustomCampaign>().Where<CustomCampaign>(x => x.Id == testVariable).Generate();
            var sqlQuery = queryObject.Query;
            var sqlParameters = queryObject.Parameters;

            var logger = new Logger(new FileWritter());

            logger.Debug(sqlQuery);
            logger.Debug(sqlQuery);

           // var sql = ExpToMsSql.Select<CustomCampaign>().InnerJoinOn<CustomCampaign, TestItem>((x, y) => x.Id == y.Id).Where<CustomCampaign>(x => x.Id == 5 || x.Id == 0);
            //Expression<Func<CustomCampaign, bool>> exp = (x) => x.CreateDate == DateTime.Now;
           // Visitor.CreateFromExpression(exp).Visit<int>("", Test);

            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }

        /*static void test(int id)
        {
            var queryObject = ExpToMsSql.Select<CustomCampaign>().Where<CustomCampaign>(x => x.Id == id);
            var sqlQuery = queryObject.Query;
            var sqlParameters = queryObject.Parameters;
        }*/

        static int Test()
        {
            return 0;
        }
    }
}
