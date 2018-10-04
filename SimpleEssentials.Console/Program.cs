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
            var objList = new List<TestItem>()
            {
                new TestItem() {Id = 0, Name = "Hello"},
                new TestItem() {Id = 1, Name = "World"},
            };
            var folderHandler = new FolderHandler();
            var fileHandler = new FileHandler();
            //var folder = folderHandler.Create("test", true);
            var file = (IFile) fileHandler.Get(
                "C:\\Users\\ksuarez\\Source\\Repos\\SimpleEssentials\\SimpleEssentials.Console\\bin\\Debug\\test\\test.xlsx");
            fileHandler.Write(file, (IEnumerable<TestItem>)objList, new ExcelWriter(), false);
            var data = fileHandler.ReadAll<TestItem>(file, new ExcelReader());

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
