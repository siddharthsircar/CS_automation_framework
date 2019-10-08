using AutomationFramework.Framework;
using AutomationFramework.Keywords;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace AutomationFramework.Pages.WebPages.HealthAssessment
{
    class Page_NewHA
    {
        SqlConnection _con;
        String pageName;
        SoftAssertions softAssertions = null;
        SqlDataReader pageinfo;
        List<string[]> pages = new List<string[]>();
        SqlDataReader questioninfo;
        List<string[]> questions = new List<string[]>();
        SqlDataReader responseinfo;
        List<string[]> responses = new List<string[]>();

        /// <summary>
        /// The constructor will calculate current class name
        /// Which will be used by ElementLocator to get 'By' object.
        /// Our repository (Or_Web.xml) contains locators of each page which will be differentiated by classname
        /// </summary>
        public Page_NewHA()
        {
            pageName = this.GetType().Name;
            Console.WriteLine("Current class : " + pageName);
        }
        public Page_NewHA(SoftAssertions softAssertions) : this()
        {
            this.softAssertions = softAssertions;
        }
        
        /// <summary>
        /// Method to click HA Start link
        /// </summary>
        private void ClickStartLink()
        {
            System.Threading.Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "startlnk");
            //SeleniumKeywords.Click(pageName, "hacontinuebtn");
        }

        /// <summary>
        /// Method to fetch module info for given HRA Id
        /// </summary>
        private void FetchModuleInfo()
        {
            SQLConnect sql = new SQLConnect();
            string query = "Select PageID , Header From Flywheel.dbo.FLY_Page Where HRAID = 89 Order by PageId asc";

            sql.OpenConnection();

            pageinfo = sql.Execute(query);
            if (pageinfo.HasRows)
            {
                while (pageinfo.Read())
                {
                    pages.Add(new string[] { pageinfo[0].ToString(), pageinfo[1].ToString() });
                }

                sql.CloseConnection();

                //Console.WriteLine("================= Page Info ==================");
                //for (int i = 0; i < pages.Count; i++)
                //{
                //    Console.WriteLine(pages.ElementAt(i)[0] + " | " + pages.ElementAt(i)[1]);
                //} 
            }
            else
            {
                Console.WriteLine(this.GetType().Name + ": No module info found");
            }
        }

        /// <summary>
        /// Method to fetch response info for given page Id
        /// </summary>
        /// <param name="pageid"></param>        
        public DataTable FetchResponseInfo(string pageid)
        {
            SQLConnect sql = new SQLConnect();
            DataTable responsetbl = new DataTable("ResponseInfo");
            try
            {
                string selectstmt = "Select RID,fr.QID,RText,fr.TypeId,frt.Description, fq.isReadOnly, fq.QText From Flywheel.dbo.FLY_Response fr ";
                string join1 = " Join Flywheel.dbo.LU_FLY_Response_Type frt on frt.TypeId = fr.TypeId";
                string join2 = " join Flywheel..FLY_Questions fq on fq.QID = fr.QID";
                string wherecond = " Where fr.QID in (Select QID from Flywheel..FLY_Questions Where PageID = " + pageid + ") and fr.TypeID in (1,2)";
                string query = selectstmt + join1 + join2 + wherecond;
                sql.OpenConnection();

                responseinfo = sql.Execute(query);

                DataTable dtSchema = responseinfo.GetSchemaTable();

                
                // You can also use an ArrayList instead of List<>
                List<DataColumn> listCols = new List<DataColumn>();

                if (dtSchema != null)
                {
                    foreach (DataRow drow in dtSchema.Rows)
                    {
                        string columnName = Convert.ToString(drow["ColumnName"]);
                        DataColumn column = new DataColumn(columnName, (Type)(drow["DataType"]));
                        column.Unique = (bool)drow["IsUnique"];
                        column.AllowDBNull = (bool)drow["AllowDBNull"];
                        column.AutoIncrement = (bool)drow["IsAutoIncrement"];
                        listCols.Add(column);
                        responsetbl.Columns.Add(column);
                    }
                }

                // Read rows from DataReader and populate the DataTable
                while (responseinfo.Read())
                {
                    DataRow dataRow = responsetbl.NewRow();
                    for (int i = 0; i < listCols.Count; i++)
                    {
                        dataRow[((DataColumn)listCols[i])] = responseinfo[i];
                    }
                    responsetbl.Rows.Add(dataRow);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                sql.CloseConnection();
            }

            Console.WriteLine("Rows Count in Responsetbl: " + responsetbl.Rows.Count);
            //string expression = "RText = 'Hours' and QText like '%Moderate%'";
            //DataRow[] foundRows;

            //// Use the Select method to find all rows matching the filter.
            //foundRows = responsetbl.Select(expression);

            //// Print column 0 of each returned row.
            //for (int i = 0; i < foundRows.Length; i++)
            //{
            //    Console.WriteLine(foundRows[i][0] + " | " + foundRows[i][1] + " | " + foundRows[i][2] + " | " + foundRows[i][3] + " | " + foundRows[i][4] + " | " + foundRows[i][5] + " | " + foundRows[i][6]);
            //}

            return responsetbl;
        }

        /// <summary>
        /// Method to fill first moduel of the HA
        /// </summary>
        /// <param name="mid"></param>
        private void FillYourInfoModule(string mid)
        {
            List<string[]> normalHadata = new List<string[]>(); 
            normalHadata = CSVReaderDataTable.GetCSVData("HAData", pageName, "yourinfo");
            Console.WriteLine("NormalHAdata count: " + normalHadata.Count);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            DataTable responsetbl = FetchResponseInfo(mid);

            for (int i = 0; i < normalHadata.Count; i++)
            {
                string qtext = normalHadata.ElementAt(i)[3];
                string rtext = normalHadata.ElementAt(i)[4];
                string inputdata = normalHadata.ElementAt(i)[5];       
                string searchcrit = "RText = '"+ rtext + "' and QText like '%" + qtext + "%'";
                string rid;
                string typedesc;
                // Use the Select method to find all rows matching the filter.
                DataRow[] foundRows = responsetbl.Select(searchcrit);

                // Print column 0 of each returned row.
                for (int j = 0; j < foundRows.Length; j++)
                {
                    Console.WriteLine("Data found for following search criteria");
                    Console.WriteLine(foundRows[j][0] + " | " + foundRows[j][1] + " | " + foundRows[j][2] + " | " + foundRows[j][3] + " | " + foundRows[j][4] + " | " + foundRows[j][5] + " | " + foundRows[j][6]);

                    rid = foundRows[j][0].ToString();
                    typedesc = foundRows[j][4].ToString();

                    if (typedesc.ToLower().Equals("radiobutton"))
                    {
                        SeleniumKeywords.Click(pageName, "responseid", rid);
                    }

                    else if (typedesc.ToLower().Equals("textbox"))
                    {
                        SeleniumKeywords.SetText(pageName, "responseid", inputdata, rid);
                    }
                }
            }
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        /// <summary>
        /// Method to verify snapshot report after first module
        /// </summary>
        public void VerifySnaphotReport()
        {
            System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            //JavaScriptKeywords.Click(pageName, "sanpshot_continuebtn");
            List<string[]> hasnapshotdata = new List<string[]>();
            hasnapshotdata = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hasnapshotreport");
            for (int i = 0; i < hasnapshotdata.Count; i++)
            {
                string elementname = hasnapshotdata.ElementAt(i)[2];
                string elementlocatorname = hasnapshotdata.ElementAt(i)[3];
                string expectedtext = hasnapshotdata.ElementAt(i)[4];
                string actualtext = SeleniumKeywords.GetText(pageName, elementlocatorname);
                //bool textmatch = actualtext.Contains(expectedtext);
                //string msg = "Element: " + elementname + "Expected: " + expectedtext + "Actual: " + actualtext;
                //result.Add(new string[] { msg, textmatch.ToString() });

                softAssertions.Add("Element : " + elementlocatorname, expectedtext, actualtext, "contains");
            }
            SeleniumKeywords.Click(pageName, "snapshot_continuebtn");

        }

        /// <summary>
        /// Method to fill About you module of the HA
        /// </summary>
        private void FillAboutYouModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "aboutyoumodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }
            //SeleniumKeywords.Click(pageName, "aboutyou_americanindian_racegroup");
            //SeleniumKeywords.Click(pageName, "aboutyou_always_safetybelt");
            //SeleniumKeywords.Click(pageName, "aboutyou_5_10years_screening");
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        /// <summary>
        /// Method to fill Your Vital module of HA
        /// </summary>
        private void FillYourVitalModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "yourvitalsmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }
            
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        /// <summary>
        /// Method to fill Diet module of the HA
        /// </summary>
        private void FillDietModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "dietmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        /// <summary>
        /// Method to fill Tobacco module of HA
        /// </summary>
        private void FillTobaccoModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "tobaccomodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "tobacco_occasionally_secondhandsmoke");
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillHistoryModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "historymodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }
            
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillActivityModule()
        {
            //System.Threading.Thread.Sleep(5000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");
            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "activitymodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "activity_morethan1year_healthylevel");
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillEmotionalHealthModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "emotionalhealthmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }
             
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillAtWorkModule()
        {
            //System.Threading.Thread.Sleep(3000);
            //SeleniumKeywords.NavigateToIFrame("assessmentFrame");

            List<string[]> haelements = new List<string[]>();

            haelements = CSVReaderDataTable.GetCSVData("HighRiskHAData", pageName, "atworkmodule");

            for (int i = 0; i < haelements.Count; i++)
            {
                string elementname = haelements.ElementAt(i)[2];
                string elementlocatorname = haelements.ElementAt(i)[3];
                string variablevalue = haelements.ElementAt(i)[4];
                SeleniumKeywords.Click(pageName, elementlocatorname, variablevalue);
            }

            //SeleniumKeywords.Click(pageName, "atwork_no_currentlyemployed");
            SeleniumKeywords.Click(pageName, "saveandcontinuebtn");
        }

        private void FillHAModules()
        {
            FetchModuleInfo();
            for (int i = 0; i < pages.Count; i++)
            {
                string moduleid = pages.ElementAt(i)[0];
                string expectedtext = pages.ElementAt(i)[1];
                Thread.Sleep(5000);
                SeleniumKeywords.NavigateToDefaultContent();
                
                string actualtext = SeleniumKeywords.GetText(pageName, "haframetitle");

                softAssertions.Add("Element : HA Module " + i + " title", expectedtext, actualtext, "contains");

                SeleniumKeywords.NavigateToIFrame("assessmentFrame");
                Thread.Sleep(3000);

                switch (i)
                {
                    case 0:
                        FillYourInfoModule(moduleid);
                        break;
                        //case 1:
                        //    VerifySnaphotReport();
                        //    break;
                        //case 2:
                        //    FillAboutYouModule();
                        //    break;
                        //case 3:
                        //    FillYourVitalModule();
                        //    break;
                        //case 4:
                        //    FillDietModule();
                        //    break;
                        //case 5:
                        //    FillTobaccoModule();
                        //    break;
                        //case 6:
                        //    FillHistoryModule();
                        //    break;
                        //case 7:
                        //    FillActivityModule();
                        //    break;
                        //case 8:
                        //    FillEmotionalHealthModule();
                        //    break;
                        //case 9:
                        //    FillAtWorkModule();
                        //    break;

                }
                Thread.Sleep(4000);
            }



        }
        private void CompleteHA()
        {
            string ha_congratulations_text = SeleniumKeywords.GetText(pageName, "ha_congratulations_text");
            //bool textmatch = ha_congratulations_text.Contains("CONGRATULATIONS!");
            //string msg = "Element: HA_Congratulation_Text Expected: CONGRATULATIONS! Actual: " + ha_congratulations_text;
            //result.Add(new string[] { msg, textmatch.ToString() });
            softAssertions.Add("Element : ha_congratulations_text", "CONGRATULATIONS!", ha_congratulations_text, "contains");

            SeleniumKeywords.NavigateToDefaultContent();
            System.Threading.Thread.Sleep(5000);
            SeleniumKeywords.Click(pageName, "hacompletedclosebtn");

        }

        private void VerifyHAIsCompleted()
        {
            System.Threading.Thread.Sleep(5000);
            string dashboard_ha_completion_percentage = SeleniumKeywords.GetText(pageName, "dashboard_ha_100per");
            //bool textmatch = dashboard_ha_completion_percentage.Equals("100%");
            //string msg = "Element: Dashboard_HA_Completion_Percentage Expected: 100% Actual: " + dashboard_ha_completion_percentage;
            //result.Add(new string[] { msg, textmatch.ToString() });

            softAssertions.Add("Element : dashboard_ha_100per", "100%", dashboard_ha_completion_percentage, "contains");

        }

        public void FillHA()
        {

            ClickStartLink();

            FillHAModules();

            //CompleteHA();
            //VerifyHAIsCompleted();

        }

        public void ValidateHAReport()
        {
            System.Threading.Thread.Sleep(3000);
            SeleniumKeywords.Click(pageName, "dashboard_ha_viewreportlink");
            System.Threading.Thread.Sleep(3000);
            List<string[]> hareports = CSVReaderDataTable.GetCSVData("CommonContent", pageName, "hareports");
            for (int i = 0; i < hareports.Count; i++)
            {
                string elementlocatorname = hareports.ElementAt(i)[3];
                string expreportdata = hareports.ElementAt(i)[4];
                string actualreportdata = SeleniumKeywords.GetText(pageName, elementlocatorname);

                //bool textmatch = actualreportdata.Trim().Contains(expreportdata.Trim());
                //string msg = "Element : " + elementlocatorname + " , Expected : " + expreportdata + " , Actual : " + actualreportdata;
                //result.Add(new string[] { msg, textmatch.ToString() });

                softAssertions.Add("Element : " + elementlocatorname, expreportdata, actualreportdata, "contains");

            }

        }
    }
}
