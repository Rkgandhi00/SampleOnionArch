using Common;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Domain
{
    public abstract class DataBaseOperations
    {
        protected async Task<List<T>> ExecuteStoreProcedure<T>(string connectionString, string commandText)
        {
            List<T> outputdata = new List<T>();
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;

                        var objSqlDataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                        outputdata = GetDataFromSqlDataReader<T>(objSqlDataReader);
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        protected async Task<List<T>> ExecuteStoreProcedure<T>(string connectionString, string commandText, Dictionary<string, object?> parameters)
        {
            List<T> outputdata = new List<T>();
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                if (param.Value != null)
                                {
                                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                                }

                            }
                        }
                        var objSqlDataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                        outputdata = GetDataFromSqlDataReader<T>(objSqlDataReader);
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        protected async Task<object> ExecuteStoreProcedureForCUD(string connectionString, string commandText, Dictionary<string, object?> parameters)
        {
            var outputdata = new object();
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                if (param.Value != null)
                                {
                                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                                }
                            }
                        }

                        outputdata = await cmd.ExecuteScalarAsync();
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        protected DataTable ExecuteStoreProcedure(string ConnectionString, string commandText, Dictionary<string, object> parameters)
        {
            try
            {
                var outputdata = new DataTable();

                var builder = new SqlConnectionStringBuilder(ConnectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                if (param.Value != null)
                                {
                                    //cmd.Parameters.AddWithValue(param.Key, param.Value.GetType().Equals(typeof(string)) ? param.Value.ToString().EscapeSQLUTF8() : param.Value);
                                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                                }

                            }
                        }

                        SqlDataReader objSqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        outputdata.Load(objSqlDataReader);
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        public List<T> GetDataFromSqlDataReader<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            try
            {
                T obj = default;
                Type t = typeof(T);

                int count = 0;
                while (dr.Read())
                {
                    if (t == typeof(string))
                    {
                        list.Add((T)dr[0]);
                    }
                    else
                    {
                        obj = Activator.CreateInstance<T>();
                        if (obj?.GetType().GetProperties().Length > 0)
                        {
                            foreach (PropertyInfo prop in obj.GetType().GetProperties())
                            {
                                if (!Equals(dr[prop.Name], DBNull.Value))
                                {
                                    prop.SetValue(obj, dr[prop.Name], null);
                                }
                            }
                            list.Add(obj);
                        }
                        else
                        {
                            list.Add((T)dr[count]);
                        }
                    }

                    count += 1;
                }
                return list;
            }
            catch (Exception ex)
            {
                throw;                
            }
        }

        private bool ColumnExists(IDataReader reader, string columnName)
        {
            return reader.GetSchemaTable().Rows.OfType<DataRow>().Any(row => row["ColumnName"].ToString().ToLower() == columnName.ToLower());

        }

        protected DataSet ExecuteStoreProcedureDs(string connectionString, string commandText, Dictionary<string, object> parameters)
        {
            try
            {
                DataSet outputdata = new DataSet();

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                if (param.Value != null)
                                {
                                    cmd.Parameters.AddWithValue(param.Key, param.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(param.Key, DBNull.Value);
                                }

                            }
                        }

                        SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(cmd);
                        objSqlDataAdapter.Fill(outputdata);
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        protected List<T> ExecuteStoreProcedureOfType<T>(string connectionString, string commandText, DataTable dt)
        {
            List<T> outputdata = new List<T>();
            try
            {


                var builder = new SqlConnectionStringBuilder(connectionString);
                builder.ConnectTimeout = Constants.DEFAULT_DATABASE_CONNECTION_TIME_OUT;
                using (var connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        var pList = new SqlParameter("@Datatable", SqlDbType.Structured);
                        pList.TypeName = "dbo.dmsDocUpload";
                        pList.Value = dt;
                        cmd.Parameters.Add(pList);

                        SqlDataReader objSqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        outputdata = GetDataFromSqlDataReader<T>(objSqlDataReader);
                    }
                }
                return outputdata;
            }
            catch (Exception ex)
            {
                //UserHelper.LogError(ex);
                throw ex;
            }
        }

        public string LogSqlCommand(SqlCommand cmd)
        {
            // Get the command text and parameter values
            string commandText = cmd.CommandText;
            SqlParameterCollection parameters = cmd.Parameters;

            // Build the parameter value string using StringBuilder
            StringBuilder paramValuesBuilder = new StringBuilder();
            foreach (SqlParameter param in parameters)
            {
                if (param.Value == null || param.Value == DBNull.Value)
                {
                    paramValuesBuilder.AppendFormat("@{0}=NULL, ", param.ParameterName);
                }
                else if (param.Value is DateTime)
                {
                    paramValuesBuilder.AppendFormat("@{0}='{1:yyyy-MM-dd HH:mm:ss.fff}', ", param.ParameterName, param.Value);
                }
                else if (param.Value.GetType() == typeof(int))
                {
                    paramValuesBuilder.AppendFormat("@{0}={1}, ", param.ParameterName, param.Value);
                }
                else
                {
                    paramValuesBuilder.AppendFormat("@{0}='{1}', ", param.ParameterName, param.Value.ToString().Replace("'", "''"));
                }
            }
            string paramValues = "";
            if (paramValuesBuilder.Length > 0)
            {
                paramValues = paramValuesBuilder.ToString(0, paramValuesBuilder.Length - 2);
            }

            // Write the command text and parameter values to the console
            return $"{commandText} {paramValues}";
        }
    }
}
