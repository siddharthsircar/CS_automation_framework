using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Pages.WebPages.Testing
{
    public class TestingAPIClass
    {
     string url = "https://sa-services2.onlifehealth.com/settings/api/admin/announcements/6810b98a-a0e4-4831-8b38-a97da0c3185e";
       // string url = "https://qb-services2.onlifehealth.com/settings/api/admin/incentivemanagementcollection/6810b98a-a0e4-4831-8b38-a97da0c3185e";

        
        /// <summary>
        /// Test method to validate API responce
        /// </summary>
        [Test]
        public void CallAnnouncement()
        {
            ApiKeywords.InitializeRequest(url);
            ApiKeywords.SetMethod("get");
            string response = ApiKeywords.SendRequest();
           
            int responsecode=ApiKeywords.GetStatusCode();
            Console.WriteLine("Response : " + responsecode);
            dynamic stuff = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
            string s = stuff[1].AnnouncementText[0].Value;

            Console.WriteLine("Response : " + response);
            Console.WriteLine("announcement=" + s);

        }


        [Test]
        public void ExecuteDBQuery()
        {
            SQLConnect sqlcn = new SQLConnect();
            sqlcn.OpenConnection();
            string joinservicecycle = "join Onlife..tbl_ServiceCycle sc on sc.ServiceCycleId = scgm.ServiceCycleId and sc.ServiceCycleEndDate > getDate()";
            string joingroupid = "join OnlifeEntity..tbl_Group g on g.GroupGuid = scgm.GroupGuid";
            string joinpointmatrix = "join Onlife..tbl_PointsMatrixActionCategoryMap pmacm on pmacm.PointsMatrixId = sc.PointsMatrixId";
            string joinrulepackage = "join Onlife.dbo.tbl_RulePackage rp on rp.RulePackageID = sc.RulePackageID";
            string joinactioncategory = "join Onlife.dbo.LU_ActionCategory ac on pmacm.ActionCategoryID = ac.ActionCategoryID";
            string joinpointsfreq = "join onlife..lu_PointsFrequency PF on pf.PointsFrequencyId = pmacm.FrequencyDays";
            //string sqlquery = "Select g.GroupId, g.GroupName,sc.ServiceCycleId,sc.PointsMatrixId, pmacm.ActionCategoryId,ac.ActionCategoryInternalName--,acm.ActionId,pmacm.InstancePoints,pmacm.MaxInstances,pmacm.frequencyMaxInstances,pmacm.FrequencyDays, pf.FrequencyDescription From Onlife..ServiceCycleGroupMap scgm"+joinservicecycle+joingroupid+joinpointmatrix+joinrulepackage+joinactioncategory+joinpointsfreq+"Where G.GroupID = 50";
            string sqlquery = "Select g.GroupName"
            + ",pmacm.InstancePoints,pmacm.frequencyMaxInstances, pf.FrequencyDescription,ac.ActionCategoryInternalName"
            + " From Onlife..ServiceCycleGroupMap scgm"
            +" join Onlife..tbl_ServiceCycle sc on sc.ServiceCycleId = scgm.ServiceCycleId and sc.ServiceCycleEndDate > getDate()"
            +" join OnlifeEntity..tbl_Group g on g.GroupGuid = scgm.GroupGuid"
            +" join Onlife..tbl_PointsMatrixActionCategoryMap pmacm on pmacm.PointsMatrixId = sc.PointsMatrixId"
            +" join Onlife.dbo.tbl_RulePackage rp on rp.RulePackageID = sc.RulePackageID"
            +" join Onlife.dbo.LU_ActionCategory ac on pmacm.ActionCategoryID = ac.ActionCategoryID"
            +" join onlife..lu_PointsFrequency PF on pf.PointsFrequencyId = pmacm.FrequencyDays"
            + " Where G.GroupName like '%nucor%'";

            DataTable responsetbl = sqlcn.Execute1(sqlquery);

            
            foreach (DataColumn col in responsetbl.Columns)
            {
                Console.Write(col.ColumnName.ToString() + "\t\t");

            }
            Console.WriteLine();
            

            foreach (DataRow row in responsetbl.Rows)
            {
                foreach (DataColumn col in responsetbl.Columns)
                    Console.Write(row[col]+"\t\t\t");
                Console.WriteLine();
            }
            
            //for (int i=0;i<responsetbl.Rows.Count;i++)
            //{
            //    for(int j=0;j<responsetbl.Columns.Count;j++)
            //    {
            //        Console.WriteLine(responsetbl.Rows[i][j]);
            //    }
            //}

        }
    }
}
