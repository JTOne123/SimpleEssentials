using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleEssentials.Console.Models;
using SimpleEssentials.IO;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO.Writers;
using SimpleEssentials.Log;
using SimpleEssentials.Log.Writters;
using SimpleEssentials.ToQuery;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //test(1);

            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }

        static void test(int id)
        {
            var queryObject = new ExpToMsSql().Select<CustomCampaign>().Where<CustomCampaign>(x => x.Exported == false).Generate();
            var sqlQuery = queryObject.Query;
            var sqlParameters = queryObject.Parameters;
        }
    }
}
