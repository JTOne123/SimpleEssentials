using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace SimpleEssentials.Console.Models
{
    [Table("OtherTable")]
    public class TestItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
