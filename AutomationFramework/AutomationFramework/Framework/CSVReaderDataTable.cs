using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    /// <summary>
    /// CSV Reader util class
    /// </summary>
    public class CSVReaderDataTable
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static ExtentTestManager Report = new ExtentTestManager();

        /// <summary>
        /// Method reads data from CSV and adds it to a custom DataTable
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static DataTable ReadCsvFile(string filename)
        {
            Console.WriteLine("filename : " + filename);
            DataTable dt = new DataTable();
            string lines;
            int i = 0;
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string proj_path = new Uri(actualPath).LocalPath;
            Console.WriteLine("Project Path : " + proj_path);
            try
            {
                using (StreamReader reader = new StreamReader(proj_path + "Resources\\Testdata\\" + filename + ".csv"))
                {
                    while (!reader.EndOfStream)
                    {

                        lines = reader.ReadLine();
                        //Console.WriteLine("FullText : " + lines);
                        //string[] rows = lines.Split(',');

                        //fulltext = reader.ReadToEnd().ToString();
                        //Console.WriteLine("FullText : "+fulltext);
                        //string[] rows = fulltext.Split('\n');
                        //Console.WriteLine("Rows len : "+rows.Length);
                        //for (int i = 0; i < rows.Count() - 1; i++)
                        //{
                        string[] rowValues = lines.Split(',');
                        if (i == 0)
                        {
                            for (int j = 0; j < rowValues.Count(); j++)
                            {
                                dt.Columns.Add(rowValues[j]);
                            }
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            for (int k = 0; k < rowValues.Count(); k++)
                            {
                                dr[k] = rowValues[k].ToString();
                            }
                            dt.Rows.Add(dr);
                        }
                        //}
                        i++;
                    }
                    //Console.WriteLine("Value of i : "+i);
                }
            }
            catch(FileNotFoundException e)
            {
                Report.Fail(e.Message);
            }
           return dt;
        }
    
        /// <summary>
        /// Method to fetch data from DataTable based on following parameter passed as arguements
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="classname"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<DataRow> GetDataRows(DataTable dt, string classname,string category)
        {
            //var filteredrows = from row in dt.AsEnumerable() where row.Field<string>("ClassName").Trim().Equals(classname) && row.Field<string>("Category").Trim().Equals(category) select row;
            var filteredrows = (from DataRow row in dt.Rows where row["ClassName"].ToString() == classname && row["Category"].ToString() == category  select row).ToList();

            //var filteredrows = from row in dt.AsEnumerable() where row.ItemArray[1].Equals(classname) && row.ItemArray[2].Equals(category) select row;
            //Console.WriteLine("FilteredRows : "+filteredrows);
            return filteredrows.ToList();
        }

        /// <summary>
        /// Method to fetch data from DataTable based on following parameter passed as arguements
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="classname"></param>
        /// <param name="category"></param>
        /// <param name="clientname"></param>
        /// <returns></returns>
        public static List<DataRow> GetDataRows(DataTable dt, string classname, string category, string clientname)
        {
            //var filteredrows = from row in dt.AsEnumerable() where row.Field<string>("ClassName").Trim().Equals(classname) && row.Field<string>("Category").Trim().Equals(category) && row.Field<string>("ClientName").Trim().Equals(clientname) select row;

            //var filteredrows = (from DataRow row in dt.Rows where row["ClassName"].ToString() == classname && row["Category"].ToString() == category && row["ClientName"].ToString() == clientname select row).ToList();
            var filteredrows = (from DataRow row in dt.Rows where row["ClientName"].ToString() == clientname && row["Category"].ToString() == category && row["ClassName"].ToString() == classname select row).ToList();

            //var filteredrows = from row in dt.AsEnumerable() where row.ItemArray[1].Equals(classname) && row.ItemArray[2].Equals(category) && row.ItemArray[0].Equals(clientname) select row;
            //var filteredrows = from row in dt.AsEnumerable() where row.ItemArray[1].Equals(classname) && row.ItemArray[2].Equals(category) && row.ItemArray[0].Equals(clientname) select row;
            // List<DataRow> filte = filteredrows;
            //Console.WriteLine("FilteredRows : "+filteredrows);
            return filteredrows.ToList();
        }

        /// <summary>
        /// Method to fetch data from DataTable based on following parameter passed as arguements
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="clientname"></param>
        /// <returns></returns>
        public static List<DataRow> GetDataRows(DataTable dt, string clientname)
        {
           
            var filteredrows = from row in dt.AsEnumerable() where row.Field<string>("ClientName").Trim().Equals(clientname) select row;
            //Console.WriteLine("FilteredRows : "+filteredrows);
            return filteredrows.ToList();
        }

        /// <summary>
        /// Method to fetch data from DataTable based on following parameter passed as arguements
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="clientname"></param>
        /// <param name="colname"></param>
        /// <returns></returns>
        public static List<DataRow> GetDataRows(DataTable dt, string clientname, string[] colname)
        {
            DataView dv = new DataView(dt);
            string[] sc = new[] { "ClientName" };
            sc = sc.Concat(colname).ToArray();
            DataTable dt1 = dv.ToTable(false, sc);
            var filteredrows = from row in dt1.AsEnumerable() where row.Field<string>("ClientName").Trim().Equals(clientname) select row;
            //Console.WriteLine("FilteredRows : "+filteredrows);
            return filteredrows.ToList();
        }
        /// <summary>
        /// Method returns the result from the DataTable in the form of list
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="classname"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static List<string[]> GetCSVData(string filename, string classname, string category)
        {
            Console.WriteLine("Classname : " + classname);
            Console.WriteLine("Category name : " + category);
            DataTable dt = ReadCsvFile(filename);
            List<DataRow> drow = GetDataRows(dt, classname, category);
            List<string[]> datalist = ConvertDrowtoList(drow);
            return datalist;
        }

        /// <summary>
        /// Method returns the result from the DataTable in the form of list
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="classname"></param>
        /// <param name="category"></param>
        /// <param name="clientname"></param>
        /// <returns></returns>
        public static List<string[]> GetCSVData(string filename, string classname, string category, string clientname)
        {
            Console.WriteLine("Classname : " + classname);
            Console.WriteLine("Category name : " + category);
            Console.WriteLine("Client name : " + clientname);
            DataTable dt = ReadCsvFile(filename);
            List<DataRow> drow = GetDataRows(dt, classname, category, clientname);
            List<string[]> datalist = ConvertDrowtoList(drow);
            return datalist;
        }

        /// <summary>
        /// Method returns the result from the DataTable in the form of list
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="clientname"></param>
        /// <returns></returns>
        public static List<string[]> GetCSVData(string filename, string clientname)
        {          
            Console.WriteLine("Client name : " + clientname);
            DataTable dt = ReadCsvFile(filename);
            List<DataRow> drow = GetDataRows(dt, clientname);
            List<string[]> datalist = ConvertDrowtoList(drow);
            return datalist;
        }

        /// <summary>
        /// Gets data from specified columns of a DataTable
        /// </summary>
        /// <param name="filename">CSV file name</param>
        /// <param name="clientname"></param>
        /// <param name="colname">Required Columns</param>
        /// <returns></returns>
        public static List<string[]> GetCSVData(string filename, string clientname, string[] colname)
        {
            Console.WriteLine("Client name : " + clientname);
            DataTable dt = ReadCsvFile(filename);
            List<DataRow> drow = GetDataRows(dt, clientname, colname);
            List<string[]> datalist = ConvertDrowtoList(drow);
            return datalist;
        }

        /// <summary>
        /// Converts DataRow result to list
        /// </summary>
        /// <param name="drow"></param>
        /// <returns></returns>
        public static List<string[]> ConvertDrowtoList(List<DataRow> drow)
        { 
            List<string[]> datalist = new List<string[]>();
            foreach (DataRow dr in drow)
            {
                string[] arr = dr.ItemArray.Cast<string>().ToArray();
                datalist.Add(arr);
            }

            return datalist;
        }

    }
}
