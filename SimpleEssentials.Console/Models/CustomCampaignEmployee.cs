using System;
using Dapper.Contrib.Extensions;

namespace SimpleEssentials.Console.Models
{
    [Table("CustomCampaignEmployee")]
    public class CustomCampaignEmployee
    {
        [Key]
        public string Employee_Id { get; set; }
        public int Campaign_Id { get; set; }
        public int Status_Id { get; set; }
        public DateTime CreateDate { get; set; }
        
        
        [Computed]
        public CustomCampaignStatus Status { get; set; }
        [Computed]
        public CustomCampaign Campaign { get; set; }
    }
}