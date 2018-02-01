using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleEssentials.Cache;
using SimpleEssentials.Console.Models;
using SimpleEssentials.DataProvider;
using SimpleEssentials.DataStore;
using SimpleEssentials.Diagnostics;
using SimpleEssentials.Extensions;
using SimpleEssentials.IO.Types;
using SimpleEssentials.IO;
using SimpleEssentials.IO.Readers;
using SimpleEssentials.Utils;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory.Container.Register<IDataStore>(() => new DbStore(Constants.DbConnectionString()));
            Factory.Container.Register<ICacheManager>(() => new MemoryCacheManager());
            var dbProvider = new DbDataProvider();

            var allCampaings = dbProvider.CreateTable<CustomCampaign>();


            ////var sql = @"SELECT * from dbo.CustomCampaign
            //            WHERE CreateDate >= @date
            //            AND Name = @name";

            //var oldWay = dbProvider.GetByParameters<CustomCampaign>(sql, new {date = DateTime.Now.AddDays(-5), name = "Testing 123"});



            //var campaigns = dbProvider.Get<CustomCampaign>(data => data.CreateDate >= DateTime.Now.AddDays(-5) && data.Name == "Testing 123");


            //var returnId = dbProvider.InsertAndReturnId(allCampaings.FirstOrDefault());


            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
