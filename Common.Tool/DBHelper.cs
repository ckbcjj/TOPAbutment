using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Common.Tool
{
    public class DBHelper
    {
        public static List<T> DateTableToObject<T>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }
            List<T> list = new List<T>();
            DataRow[] rowArray = dt.Select();
            Dictionary<string, PropertyInfo> dictionary = typeof(T).GetProperties().ToDictionary<PropertyInfo, string>(k => k.Name.ToUpper());
            PropertyInfo info = null;
            foreach (DataRow row in rowArray)
            {
                T item = Activator.CreateInstance<T>();
                list.Add(item);
                foreach (DataColumn column in row.Table.Columns)
                {
                    if (dictionary.TryGetValue(column.ColumnName.ToUpper(), out info))
                    {
                        if (row[column] is DBNull)
                        {
                            Type infoType = info.PropertyType;
                            info.SetValue(item, Convert.ChangeType(row[column], infoType), null);
                        }
                        else
                        {
                            
                        info.SetValue(item, row[column], null);
                        }
                    }
                }
            }
            return list;
        }

        public static SqlParameter[] DicToParameters(Dictionary<string, object> dic)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            foreach (KeyValuePair<string, object> pair in dic)
            {
                SqlParameter item = new SqlParameter {
                    ParameterName = pair.Key,
                    Value = pair.Value ?? DBNull.Value
                };
                list.Add(item);
            }
            return list.ToArray();
        }

        public static Dictionary<string, object> ObjectTodic<T>(T obj)
        {
            Type type = obj.GetType();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (PropertyInfo info in type.GetProperties())
            {
                dictionary[info.Name] = info.GetValue(obj, null);
            }
            return dictionary;
        }
    }
}

