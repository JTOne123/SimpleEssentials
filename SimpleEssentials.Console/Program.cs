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


            Factory.Log.Info("Just a test log");

            var dbProvider = new DbDataProvider();
            var campaigns = new List<CustomCampaign>()
            {
                new CustomCampaign() {Description = "Desc1", Name = "Test1"},
                new CustomCampaign() {Description = "Desc2", Name = "Test2"},
                new CustomCampaign() {Description = "Desc3", Name = "Test3"},
            };
            var rowsAffected = dbProvider.InsertList(campaigns);
            System.Console.WriteLine(rowsAffected);


            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
