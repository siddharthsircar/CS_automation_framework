using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Framework
{
    public class CSVReader
    {
        private static ExtentTestManager Report = new ExtentTestManager();
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static List<string[]> GetCsvData(string filename)
        {
            List<string[]> csv_data = new List<string[]>();

            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string proj_path = new Uri(actualPath).LocalPath;
            Console.WriteLine("Project Path : " + proj_path);
            try
            {
                using (var reader = new StreamReader(proj_path + "Resources\\Testdata\\" + filename + ".csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var lines = reader.ReadLine();
                        var values = lines.Split(',');
                        csv_data.Add(values);
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Report.Info(e.Message);
            }
            return csv_data;
        }

        internal static List<string[]> GetCsvData(string v1, string currentClassName, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
