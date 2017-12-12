using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEssentials.Console.Models
{
    [Table("Client")]
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }

    }
}
