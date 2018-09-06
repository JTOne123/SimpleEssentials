using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimpleEssentials.Console.Models;
using SimpleEssentials.LinqToDb;

namespace SimpleEssentials.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = DateTime.Now;
            var sql = LinqToSql.Select<CustomCampaign>().Where<CustomCampaign>(x => x.CreateDate == DateTime.Now);

            System.Console.WriteLine("Press Enter to Exit");
            System.Console.ReadLine();
        }
    }
}
