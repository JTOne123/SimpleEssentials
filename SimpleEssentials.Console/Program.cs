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
            //Factory.RegisterDefaults();
            //Factory.Container.Register<IDataStore>(() => new DbStore(Constants.DbConnectionString()));
            //Factory.Container.Register<ICacheManager>(() => new MemoryCacheManager());
            //Factory.Container.Register<ILogFileHandler>(() => new LogFileByDateHandler());
            //Factory.Container.Verify();


            Factory.Log.Info("Just a test log");

            //var dbProvider = new DbDataProvider();

            //var dbProvider = new DbDataProvider(new DbStore("[CONTECTION_STRING]"), new MemoryCacheManager());


            //for (int j = 0; j < 10; j++)
            //{
            //    var watch = System.Diagnostics.Stopwatch.StartNew();
            //    for (int i = 0; i < 100000; i++)
            //    {
            //        var results = dbProvider.GetByType<CustomCampaign>(new CacheSettings() { Key = "CAMPAIGNS", LifeSpan = (new TimeSpan(0, 1, 0, 0)), StorageType = CacheStorage.Hashed });
            //    }
            //    watch.Stop();
            //    var elapsedMs = watch.ElapsedMilliseconds;
            //    System.Console.WriteLine($"Elapsed Time: {elapsedMs}");
            //}

            //Thread.Sleep(10000);

            //for (int j = 0; j < 10; j++)
            //{
            //    var watch = System.Diagnostics.Stopwatch.StartNew();
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        var results = dbProvider.GetByType<CustomCampaignEmployee>(new CacheSettings() { Key = "EMPLOYEE_CAMPAINS", LifeSpan = (new TimeSpan(0, 1, 0, 0)), StorageType = CacheStorage.Hashed });
            //        var emp = dbProvider.Get<CustomCampaignEmployee>("  1509394", new CacheSettings() { Key = "EMPLOYEE_CAMPAINS", LifeSpan = (new TimeSpan(0, 1, 0, 0)), StorageType = CacheStorage.Hashed });
            //        //var emp = results.FirstOrDefault(x => x.Employee_Id == "  1509394");
            //        //System.Console.WriteLine($"Employee: {emp.Employee_Id}");
            //    }
            //    watch.Stop();
            //    var elapsedMs = watch.ElapsedMilliseconds;
            //    System.Console.WriteLine($"Elapsed Time: {elapsedMs}");
            //}





            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
