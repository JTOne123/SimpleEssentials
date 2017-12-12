using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Console.Models
{
    [Table("FireDrillMonthlyClients")]
    public class FireDrillMonthlyClient
    {
        public int Id { get; set; }
        public int MonthlyLogId { get; set; }
        public int ClientId { get; set; }
        public DateTime CreateDate { get; set; }

        [Computed]
        public Client Client { get; set; }
    }
}
