using System;
using Dapper.Contrib.Extensions;

namespace SimpleEssentials.Console.Models
{
    [Table("CustomCampaignYooo")]
    public class CustomCampaign
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Exported { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}