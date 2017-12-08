using System;
using System.Collections.Generic;
using System.Linq;
using CrackerBarrel.Foundation.Cache;
using CrackerBarrel.Foundation.Console.Models;
using CrackerBarrel.Foundation.DataProvider;
using CrackerBarrel.Foundation.DataStore;

namespace CrackerBarrel.Foundation.Console
{
    public class CustomCampaignRepository
    {
        private readonly IDbProvider _dbProvider;

        public CustomCampaignRepository()
        {
            _dbProvider = new DbDataProvider(new DbStore(Constants.DbConnectionString()), new SessionCacheManager());
        }

        public IEnumerable<CustomCampaignEmployee> GetEmployeeStatuses(string empId)
        {
            string sql = @"SELECT * FROM dbo.CustomCampaignEmployee cce
                           inner join dbo.CustomCampaign  cc on cce.campaign_id = cc.id
                           inner join dbo.CustomCampaignStatus ccs on cce.status_id = ccs.id
                           WHERE employee_id = @employeeId";

            return _dbProvider.GetMultiMap<CustomCampaignEmployee, CustomCampaign, CustomCampaignStatus>(sql, (status, campaign, statusCode) =>
            {
                status.Campaign = campaign;
                status.Status = statusCode;
                return status;
            }, new {employeeId = empId}, "EMPLOYEE_STATUS");
        }

        public CustomCampaignEmployee GetLatestStatus(string empId, int campaignId)
        {
            string sql = @"select cce.employee_id, cce.campaign_id, cce.status_id, 
                            cce.createdate, cc.id, cc.name, cc.[description], 
                            cc.createdate, ccs.id, ccs.name, ccs.[description], ccs.createdate
                            from dbo.CustomCampaignEmployee cce
                            inner join (
                                select employee_id, max(createdate) as MaxDate
                                from dbo.CustomCampaignEmployee
                                where employee_id = @employeeId and campaign_id = @campaignId
	                            group by employee_id
                            ) tm on cce.employee_id = tm.employee_id and cce.createdate = tm.MaxDate
                            inner join dbo.CustomCampaign  cc on cce.campaign_id = cc.id
                            inner join dbo.CustomCampaignStatus ccs on cce.status_id = ccs.id";

            return _dbProvider.GetMultiMap<CustomCampaignEmployee, CustomCampaign, CustomCampaignStatus>(sql, (status, campaign, statusCode) =>
            {
                status.Campaign = campaign;
                status.Status = statusCode;
                return status;
            }, new { employeeId = empId, campaignId = campaignId }, "EMPLOYEE_STATUS").FirstOrDefault();
        }



        public bool SaveInteraction(string empId, int campaignId, int statusId)
        {
            var empCampign = new CustomCampaignEmployee
            {
                Employee_Id = empId,
                Campaign_Id = campaignId,
                Status_Id = statusId,
                CreateDate = DateTime.Now
            };

            return _dbProvider.Add(empCampign, "EMPLOYEE_STATUS");
        }
    }
}