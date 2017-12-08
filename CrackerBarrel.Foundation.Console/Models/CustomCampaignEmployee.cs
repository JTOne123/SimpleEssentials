using System;
using Dapper.Contrib.Extensions;

namespace CrackerBarrel.Foundation.Console.Models
{
    [Table("CustomCampaignEmployee")]
    public class CustomCampaignEmployee
    {
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