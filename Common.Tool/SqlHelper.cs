using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Common.Tool
{
    public static class SqlHelper
    {

        public static string conStr = System.Configuration.ConfigurationManager.AppSettings["conStr"];
        public static string conStrRead = System.Configuration.ConfigurationManager.AppSettings["conStrRead"];

        public static bool BulkToDataBase<T>(IEnumerable<T> list, SqlConnection connection, string tableName)
        {
            SqlTransaction sqlbulkTransaction = connection.BeginTransaction();
            bool flag = BulkToDataBase<T>(list, sqlbulkTransaction, connection, tableName);
            if (flag)
            {
                sqlbulkTransaction.Commit();
                return flag;
            }
            sqlbulkTransaction.Rollback();
            return flag;
        }

        public static bool BulkToDataBase<T>(IEnumerable<T> list, string connectStr, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(connectStr))
            {
                connection.Open();
                SqlTransaction sqlbulkTransaction = connection.BeginTransaction();
                bool flag = BulkToDataBase<T>(list, sqlbulkTransaction, connection, tableName);
                if (flag)
                {
                    sqlbulkTransaction.Commit();
                }
                else
                {
                    sqlbulkTransaction.Rollback();
                }
                connection.Close();
                return flag;
            }
        }

        public static bool BulkToDataBase(DataTable dt, string tableName, string connectStr)
        {
            using (SqlConnection connection = new SqlConnection(connectStr))
            {
                connection.Open();
                bool flag = BulkToDataBase(connection, tableName, dt, null);
                connection.Close();
                return flag;
            }
        }

        public static bool BulkToDataBase<T>(IEnumerable<T> list, SqlTransaction sqlbulkTransaction, SqlConnection connection, string tableName)
        {
            DataTable dt = ObjectToDataTable<T>(list);
            return BulkToDataBase(connection, tableName, dt, sqlbulkTransaction);
        }

        public static bool BulkToDataBase(DataTable dt, string tableName, SqlConnection connection, SqlTransaction transaction)
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                return BulkToDataBase(connection, tableName, dt, transaction);
            }
            catch
            {
                return false;
            }
        }

        public static bool BulkToDataBase(SqlConnection connection, string tableName, DataTable dt, SqlTransaction sqlbulkTransaction = null)
        {
            SqlBulkCopy copy = null;
            if (sqlbulkTransaction == null)
            {
                copy = new SqlBulkCopy(connection);
            }
            else
            {
                copy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlbulkTransaction);
            }
            try
            {
                copy.BatchSize = (dt.Rows.Count > 0x4e20) ? 0x7d0 : 800;
                copy.DestinationTableName = tableName;
                foreach (DataColumn column in dt.Columns)
                {
                    copy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(column.ColumnName, column.ColumnName));
                }
                copy.WriteToServer(dt);
                copy.Close();
                return true;
            }
            catch
            {
                copy.Close();
                return false;
            }
        }

        private static void ComandSet(SqlCommand cmd, SqlConnection con, CommandType commandType, string sql, SqlParameter[] parameters = null)
        {
            cmd.Connection = con;
            cmd.CommandType = commandType;
            cmd.CommandText = sql;
            if ((parameters != null) && parameters.Any<SqlParameter>())
            {
                cmd.Parameters.AddRange(parameters);
            }
        }

        public static DataTable ExcuteDataTable(string sql, SqlParameter[] parameters, string conS = null)
        {
            DataTable table2;
            DataTable dataTable = new DataTable();
            if (conS == null)
            {
                conS = conStr;
            }
            using (SqlConnection connection = new SqlConnection(conS))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    ComandSet(command, connection, CommandType.Text, sql, parameters);
                    connection.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                        connection.Close();
                        table2 = dataTable;
                    }
                }
            }
            return table2;
        }

        public static T ExecuteDataReader<T>(SqlDataReader dr)
        {
            T obj = default(T);
            Type type = typeof(T);
            PropertyInfo[] propertyInfos = type.GetProperties();
            int columnCount = dr.FieldCount;
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                string propertyName = propertyInfo.Name;
                for (int i = 0; i < columnCount; i++)
                {
                    string columnName = dr.GetName(i);
                    if (string.Compare(propertyName, columnName, true) == 0)
                    {
                        object value = dr.GetValue(i);
                        if (value != null && value != DBNull.Value)
                        {
                            propertyInfo.SetValue(obj, value, null);
                        }
                        break;
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 执行SQL代码
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="conS"></param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType, SqlParameter[] paras)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                result = ExecuteNonQuery(sql, commandType, paras, connection);
                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// 执行SQL代码
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="conS"></param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters, string conS = null)
        {
            int num;
            if (conS == null)
            {
                conS = conStr;
            }
            using (SqlConnection connection = new SqlConnection(conS))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandTimeout = 6000;
                    ComandSet(command, connection, CommandType.Text, sql, parameters);
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
            }
            return num;
        }

        /// <summary>
        /// 执行SQL代码
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="conS"></param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters, SqlConnection connection, SqlTransaction transaction)
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Transaction = transaction;
                ComandSet(command, connection, CommandType.Text, sql, parameters);
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行SQL代码
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="conS"></param>
        /// <returns>返回受影响行数</returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType, SqlParameter[] paras, SqlConnection connection)
        {
            SqlCommand command = CreateCommandHelper(sql, commandType, paras, connection);
            int result = command.ExecuteNonQuery();
            command.Dispose();

            return result;
        }

        public static int ExecuteNonQueryProc(string name, Dictionary<string, object> @params, string conS)
        {
            int num;
            if (conS == null)
            {
                conS = conStr;
            }
            using (SqlConnection connection = new SqlConnection(conS))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandTimeout = 0;
                    ComandSet(command, connection, CommandType.StoredProcedure, name, DBHelper.DicToParameters(@params));
                    connection.Open();
                    num = command.ExecuteNonQuery();
                }
            }
            return num;
        }

        public static object ExecuteScalar(string sql, SqlParameter[] parameters, string conS = null)
        {
            object obj2;
            new DataTable();
            if (conS == null)
            {
                conS = conStr;
            }
            using (SqlConnection connection = new SqlConnection(conS))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    ComandSet(command, connection, CommandType.Text, sql, parameters);
                    connection.Open();
                    obj2 = command.ExecuteScalar();
                }

            }
            return obj2;
        }

        public static DataTable ObjectToDataTable<T>(IEnumerable<T> list)
        {
            DataTable table = new DataTable();
            Type type = typeof(T);
            T[] source = (list as T[]) ?? list.ToArray<T>();
            if (((typeof(object) == type) && (list != null)) && source.Any<T>())
            {
                type = source.First<T>().GetType();
            }
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                DataColumn column = new DataColumn(info.Name);
                if (info.PropertyType.IsGenericType)
                {
                    if (!(info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        continue;
                    }
                    column.DataType = info.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    column.DataType = info.PropertyType;
                }
                table.Columns.Add(column);
            }
            DataColumnCollection columns = table.Columns;
            foreach (T local in source)
            {
                DataRow row = table.NewRow();
                IEnumerator enumerator = columns.GetEnumerator();

                Func<PropertyInfo, bool> predicate = null;
                DataColumn c;
                while (enumerator.MoveNext())
                {
                    c = (DataColumn)enumerator.Current;
                    if (predicate == null)
                    {
                        predicate = p => p.Name == c.ColumnName;
                    }
                    PropertyInfo info2 = properties.FirstOrDefault<PropertyInfo>(predicate);
                    if (info2 != null)
                    {
                        row[c] = info2.GetValue(local, null) ?? DBNull.Value;
                    }
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static void UpdateBulk(string connString, DataTable table)
        {
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand comm = conn.CreateCommand();
            comm.CommandTimeout = 0;
            comm.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(comm);
            SqlCommandBuilder commandBulider = new SqlCommandBuilder(adapter);
            commandBulider.ConflictOption = ConflictOption.OverwriteChanges;
            try
            {
                conn.Open();
                //设置批量更新的每次处理条数 
                adapter.UpdateBatchSize = 5000;
                adapter.SelectCommand.Transaction = conn.BeginTransaction();/////////////////开始事务 
                if (table.ExtendedProperties["SQL"] != null)
                {
                    adapter.SelectCommand.CommandText = table.ExtendedProperties["SQL"].ToString();
                }
                adapter.Update(table);
                adapter.SelectCommand.Transaction.Commit();/////提交事务 
            }
            catch (Exception ex)
            {
                if (adapter.SelectCommand != null && adapter.SelectCommand.Transaction != null)
                {
                    adapter.SelectCommand.Transaction.Rollback();
                }
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        #region Method ExecuteDataTable

        public static DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(commandText, CommandType.Text, null);
        }

        public static DataTable ExecuteDataTable(string commandText, SqlParameter[] paras)
        {

            return ExecuteDataTable(commandText, CommandType.Text, paras);
        }

        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] paras)
        {
            DataTable dt = null;
            using (SqlConnection connection = new SqlConnection(SqlHelper.conStr))
            {
                connection.Open();
                dt = ExecuteDataTable(commandText, commandType, paras, connection);
                connection.Close();
            }

            return dt;
        }

        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] paras, SqlConnection connection)
        {
            DataTable dt = new DataTable();
            SqlCommand command = CreateCommandHelper(commandText, commandType, paras, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            command.Dispose();
            adapter.Dispose();

            return dt;
        }

        public static DataTable ExecuteDataTable(string commandText, CommandType commandType, SqlParameter[] paras, SqlTransaction trans)
        {
            DataTable dt = new DataTable();
            SqlCommand command = CreateCommandHelper(commandText, commandType, paras, trans);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dt);
            command.Dispose();
            adapter.Dispose();

            return dt;
        }

        #endregion

        #region Private Method CreateCommandHelper

        private static SqlCommand CreateCommandHelper(string commandText, CommandType commandType, SqlParameter[] paras, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Connection = connection;

            if (paras != null && paras.Length > 0)
            {
                foreach (SqlParameter p in paras)
                {
                    /*Update 修改无法使用 ParameterDirection.Output 来输出值的Bug*/
                    //SqlParameter paraNew = new SqlParameter();
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        /*
                        paraNew.ParameterName = p.ParameterName;
                        paraNew.SqlDbType = p.SqlDbType;
                        paraNew.DbType = p.DbType;
                        paraNew.SourceColumn = p.SourceColumn;
                        paraNew.Value = p.Value;
                         */

                        command.Parameters.Add(p);
                    }

                }
            }

            return command;
        }

        private static SqlCommand CreateCommandHelper(string commandText, CommandType commandType, SqlParameter[] paras, SqlTransaction trans)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.Connection = trans.Connection;
            command.Transaction = trans;

            if (paras != null && paras.Length > 0)
            {
                foreach (SqlParameter p in paras)
                {
                    SqlParameter paraNew = new SqlParameter();
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }

                        paraNew.ParameterName = p.ParameterName;
                        paraNew.SqlDbType = p.SqlDbType;
                        paraNew.DbType = p.DbType;
                        paraNew.SourceColumn = p.SourceColumn;
                        paraNew.Value = p.Value;
                    }
                    command.Parameters.Add(paraNew);
                }
            }

            return command;
        }

        #endregion

        #region Method ExecuteList
        public static List<T> ExecuteList<T>(string commandText)
        {
            return ExecuteList<T>(commandText, CommandType.Text, null);
        }

        public static List<T> ExecuteList<T>(string commandText, SqlParameter[] paras)
        {
            return ExecuteList<T>(commandText, CommandType.Text, paras);
        }

        public static List<T> ExecuteList<T>(string commandText, CommandType commandType, SqlParameter[] paras)
        {
            List<T> list = new List<T>();
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                connection.Open();
                list = ExecuteList<T>(commandText, commandType, paras, connection);
                connection.Close();
            }

            return list;

        }

        public static List<T> ExecuteList<T>(string commandText, CommandType commandType, SqlParameter[] paras, SqlConnection connection)
        {
            List<T> list = new List<T>();

            SqlCommand command = CreateCommandHelper(commandText, commandType, paras, connection);
            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    T obj = ExecuteDataReader<T>(dr);
                    list.Add(obj);
                }
            }
            command.Dispose();

            return list;
        }

        public static List<T> ExecuteList<T>(string commandText, CommandType commandType, SqlParameter[] paras, SqlTransaction trans)
        {
            List<T> list = new List<T>();

            SqlCommand command = CreateCommandHelper(commandText, commandType, paras, trans);
            using (SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    T obj = ExecuteDataReader<T>(dr);
                    list.Add(obj);
                }
            }
            command.Dispose();

            return list;
        }
        #endregion
    }
}

