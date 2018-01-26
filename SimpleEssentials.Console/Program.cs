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

            var campaigns = dbProvider.Get<CustomCampaign>(x => x.CreateDate >= DateTime.Now.AddDays(-5) && !x.Test);


            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
