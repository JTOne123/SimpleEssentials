using SimpleEssentials.Console.Models;
using SimpleEssentials.DataProvider;
using SimpleEssentials.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Console
{
    class TestRepository
    {
        private readonly IDbProvider _dbProvider;
        public TestRepository()
        {
            _dbProvider = new DbDataProvider(new DbStore(Constants.DbConnectionString()), null);
        }
        public IEnumerable<FireDrillMonthlyClient> GetClients(int logId)
        {
            string sql = @"select fmc.Id, fmc.MonthlyLogId, fmc.CreateDate, c.* from dbo.FireDrillMonthlyClients fmc
                            inner join dbo.Client c on fmc.ClientId = c.ClientID
                            where fmc.MonthlyLogId = @logId";

            return _dbProvider.GetMultiMap<FireDrillMonthlyClient, Client>(sql,
                (logClient, client) =>
                {
                    logClient.Client = client;
                    return logClient;
                }, new { logId = logId }, "ClientID");

        }
    }
}
