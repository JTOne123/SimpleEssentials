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
using SimpleEssentials.IO;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.IO.Types;
using SimpleEssentials.Log;
using SimpleEssentials.Log.Writters;
using SimpleEssentials.ToQuery;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileHandler = new FileHandler();
            var file = (IFile)fileHandler.Get("PATH");
            var data = fileHandler.ReadAll<ExcelModel>(file, new ExcelReader());

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
