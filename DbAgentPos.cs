using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.IO;
using Dapper;
using System.ComponentModel;

namespace WMS_API
{
    class DBAgentPos
    {
        
        string m_strSettingPath;

        public DBAgentPos(IConfiguration configuration)
        {
            IP =configuration["DB:IP"];
            PORT = configuration["DB:PORT"];
            UID = configuration["DB:UID"];
            PWD = configuration["DB:PWD"]; 
            TableUserName = configuration["DB:TableUserName"]; 
        }

        // public DBAgentPos()
        // {
        //     m_strSettingPath = SettingFilePath;
        //     LoadDefaultSetting();
        // }

        public string m_ConnectionString
        {
            get
            {
                return string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", IP, PORT, UID, PWD, TableUserName);
            }
        }

        public NpgsqlConnection GetConnection
        {
            get
            {
                NpgsqlConnection pNpgsqlConnection = null;
                pNpgsqlConnection = new NpgsqlConnection(m_ConnectionString);
                pNpgsqlConnection.Open();
                return pNpgsqlConnection;
            }
        }

        private string SettingFilePath
        {
            get
            {
                return (string.IsNullOrEmpty(GetCurrentDllFloderPath()) ? "system.ini" : GetCurrentDllFloderPath() + @"\system.ini");
            }
        }

        public static string GetCurrentDllFloderPath()
        {
            var dllPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            var dllFloderPath = Path.GetDirectoryName(dllPath).Replace("file:\\", string.Empty);

            return dllFloderPath;
        }

        public Boolean ExecuteNonQueryNoCatch(string sql, int timeout = 300)
        {
            Boolean bSuccess = true;
            NpgsqlConnection connection = null;
            NpgsqlCommand command = null;
            Exception exp = null;

            try
            {
                connection = new NpgsqlConnection(m_ConnectionString);
                connection.Open();
                command = new NpgsqlCommand(sql, connection);
                command.CommandTimeout = timeout;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                bSuccess = false;
                exp = ex;
                
            }
            finally
            {
                if (command != null)
                {
                    command.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            if (!bSuccess)
            {
                throw exp;
            }

            return bSuccess;
        }

        public DataTable QueryReturnDTNoCatch(string sql, int timeout = 300)
        {
            String strErrorMsg = String.Empty;
            DataTable res = new DataTable();
            NpgsqlConnection connection = null;
            NpgsqlDataAdapter adapter = null;
            Boolean bSuccess = true;
            Exception exp = null;

            try
            {
                connection = new NpgsqlConnection(m_ConnectionString);
                adapter = new NpgsqlDataAdapter(sql, connection);
                adapter.SelectCommand.CommandTimeout = timeout;
                adapter.Fill(res);
            }
            catch (Exception ex)
            {
                res.Clear();
                strErrorMsg = String.Empty;
                strErrorMsg = ex.ToString();
                bSuccess = false;
                exp = ex;

                if (!sql.Contains("1=0"))
                {
                    // ProgramLog.WriteLineLog(string.Format("QueryReturnDTNoCatch Exception!! SQL: {0}, Msg: {1}", sql, ex.Message));
                }
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            if (!bSuccess)
            {
                throw exp;
            }

            return res;
        }

        public DataTable QueryReturnDT(string sql, int timeout = 300)
        {
            String strErrorMsg = String.Empty;
            DataTable res = new DataTable();
            NpgsqlConnection connection = null;
            NpgsqlDataAdapter adapter = null;
            Boolean bSuccess = true;
            Exception exp = null;

            try
            {
                connection = new NpgsqlConnection(m_ConnectionString);
                adapter = new NpgsqlDataAdapter(sql, connection);
                adapter.SelectCommand.CommandTimeout = timeout;
                adapter.Fill(res);
            }
            catch (Exception ex)
            {
                res.Clear();
                strErrorMsg = String.Empty;
                strErrorMsg = ex.ToString();
                bSuccess = false;
                exp = ex;

                if (!sql.Contains("1=0"))
                {
                    // ProgramLog.WriteLineLog(string.Format("QueryReturnDTNoCatch Exception!! SQL: {0}, Msg: {1}", sql, ex.Message));
                }
            }
            finally
            {
                if (adapter != null)
                {
                    adapter.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return res;
        }

        public List<dynamic> QueryReturnListDynamic(string sql, object para = null)
        {
            List<dynamic> res = new List<dynamic>();
            String strErrorMsg = String.Empty;
            NpgsqlConnection connection = null;
            Boolean bSuccess = true;
            Exception exp = null;

            try
            {
                connection = new NpgsqlConnection(m_ConnectionString);

                if (para != null)
                {
                    res = connection.Query(sql, para).ToList();
                }
                else
                {
                    res = connection.Query(sql).ToList();
                }
            }
            catch (Exception ex)
            {
                strErrorMsg = String.Empty;
                strErrorMsg = ex.ToString();
                bSuccess = false;
                exp = ex;

                if (!sql.Contains("1=0"))
                {
                    // ProgramLog.WriteLineLog(string.Format("QueryReturnDTNoCatch Exception!! SQL: {0}, Msg: {1}", sql, ex.Message));
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            if (!bSuccess)
            {
                throw exp;
            }

            return res;
        }

        // public void InsertWithBinaryImport(string TableName, Dictionary<string, object> Dic)
        // {
        //     Boolean bSuccess = true;

        //     NpgsqlConnection pNpgsqlConnection = null;

        //     try
        //     {
        //         pNpgsqlConnection = new NpgsqlConnection(m_ConnectionString);

        //         pNpgsqlConnection.Open();

        //         TableName = TableName.ToLower();

        //         string transFieldList = String.Join(",", Dic.Keys);

        //         string cmd = string.Format(@"COPY {0}({1}) FROM STDIN BINARY", TableName, transFieldList);

        //         using (var writer = pNpgsqlConnection.BeginBinaryImport(cmd))
        //         {
        //             writer.StartRow();
        //             writer.Write("1");
        //             writer.Write(DateTime.Now.Offset(), NpgsqlTypes.NpgsqlDbType.Date);
        //             writer.Write(25, NpgsqlTypes.NpgsqlDbType.Integer);
        //             writer.Write(1, NpgsqlTypes.NpgsqlDbType.Integer);
        //             writer.Complete();
        //         }

        //     }
        //     catch (Exception ex)
        //     {

        //     }



        // }

        public bool CreateRawTable(string EqName, string TableName)
        {
            bool bSuccess = false;
            try
            {
                string sql = string.Format(@"CREATE TABLE public.pichistory_{0}_{1} (
	                                            id text NOT NULL,
	                                            timetag timestamp NULL,
	                                            max_pos int4 NULL,
	                                            max_value float4 NULL,
	                                            filepath text NULL,
	                                            CONSTRAINT pkey_pichistory_{0}_{1} PRIMARY KEY (id)
                                            );
                                            CREATE INDEX idx_pichistory_{0}_{1}_timetag ON public.pichistory_{0}_{1} USING btree (timetag);",
                                                EqName, TableName
                                                );
                bSuccess = ExecuteNonQueryNoCatch(sql);

                return bSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

     

        public bool TableExist(string TableName)
        {
            try
            {
                string sql = string.Format(@"select relname from pg_class where relname = '{0}';", TableName);

                DataTable queryDT = QueryReturnDT(sql);

                if (queryDT.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string IP
        {
            get;
            set;
        }

        public string PORT
        {
            get;
            set;
        }

        public string UID
        {
            get;
            set;
        }

        public string PWD
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }

        public string EndTime
        {
            get;
            set;
        }

        public string TableUserName
        {
            get;
            set;
        }
    }
}
