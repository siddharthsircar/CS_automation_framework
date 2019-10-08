using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AutomationFramework.Framework
{
    class SQLConnect
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
        string connectionstring = null;
        public void OpenConnection()
        {                        
            string servername = ConfigurationManager.AppSettings["servername"];
            string username = ConfigurationManager.AppSettings["sqlusername"];
            string password = ConfigurationManager.AppSettings["sqlpassword"];

            connectionstring = "Data Source="+servername+";User ID="+username+";Password="+password;
            connection = new SqlConnection(connectionstring);

            try
            {
                connection.Open();
                Console.WriteLine("Connection Open ! ");                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
                Console.WriteLine("Fatal Error: \n" + ex);
            }
        }

        public SqlDataReader Execute(string sql)
        {
            command = new SqlCommand(sql, connection);
            try
            {
                dataReader = command.ExecuteReader();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while fetching data: " + e);
            }            
            return dataReader;
        }
        
        public void ExecuteStoreProcedure()
        {
            //SqlCommand cmd = new SqlCommand("", connection);
            //cmd.CommandType = CommandType.StoredProcedure;
            //try
            //{
            //    dataReader = command.ExecuteReader();

            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine("Exception while fetching data: " + e);
            //}

            FileInfo file = new FileInfo("D:\\SeleniumFramework-SA\\insertDeviceData.sql");
            string SQLscript = file.OpenText().ReadToEnd();

            //SQLscript = SQLscript.Replace("GO", "");

            //Optional to Replace Comments with empty string

            SQLscript = Regex.Replace(SQLscript, "([/*][*]).*([*][/])", "");

            //Optional to Replace Chain of spaces with one Space

            SQLscript = Regex.Replace(SQLscript, "\\s{2,}", " ");

            SqlCommand sqlCommand = new SqlCommand(SQLscript,connection);

            sqlCommand.CommandType = CommandType.Text;
            
            // sqlCommand.Connection.Open();
            try
            {
                dataReader = sqlCommand.ExecuteReader();
               // string col1Value = dataReader[0].ToString();
                //Console.WriteLine("Result :  " + col1Value);

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while fetching data: " + e);
            }
            //sqlCommand.ExecuteNonQuery();
            //sqlCommand.Connection.Close();

        }

        public DataTable Execute1(string sql)
        {
            DataTable responsetbl = null;
            command = new SqlCommand(sql, connection);
            try
            {
                dataReader = command.ExecuteReader();
                responsetbl = GetDataTableFromSqlDataReader(dataReader);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while fetching data: " + e);
            }
            return responsetbl;
        }

        public DataTable GetDataTableFromSqlDataReader(SqlDataReader dataReader)
        {
            DataTable responsetbl = new DataTable("ResponseInfo");
            DataTable dtSchema = dataReader.GetSchemaTable();


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
            while (dataReader.Read())
            {
                DataRow dataRow = responsetbl.NewRow();
                for (int i = 0; i < listCols.Count; i++)
                {
                    dataRow[((DataColumn)listCols[i])] = dataReader[i];
                }
                responsetbl.Rows.Add(dataRow);
            }

            return responsetbl;
        }

        public void CloseConnection()
        {
            try
            {
                command.Dispose();
                connection.Close();
                Console.WriteLine("Connection Closed ! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not close connection ! ");
                Console.WriteLine("Fatal Error: \n"+ex);
            }
        }
    }
}

