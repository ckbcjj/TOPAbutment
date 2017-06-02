using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace Common.Tool
{
    public class XmlHelper
    {
        public static string OutExcel<T>(List<T> list, Dictionary<string, string> column, string filename)
        {
            if (((list == null) || (list.Count == 0)) || ((column == null) || (column.Count == 0)))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            string str = string.Empty;
            foreach (KeyValuePair<string, string> pair in column)
            {
                str = str + pair.Value + "\t";
            }
            str = str.Substring(0, str.LastIndexOf("\t"));
            builder.Append(str);
            Type type = typeof(T);
            BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance;
            PropertyInfo[] properties = type.GetProperties(bindingAttr);
            foreach (T local in list)
            {
                StringBuilder builder2 = new StringBuilder();
                foreach (string str2 in column.Keys)
                {
                    foreach (PropertyInfo info in properties)
                    {
                        if (str2 == info.Name)
                        {
                            PropertyInfo property = local.GetType().GetProperty(info.Name);
                            if (property != null)
                            {
                                object obj2 = property.GetValue(local, null);
                                builder2.Append(((obj2 == null) ? string.Empty : obj2) + "\t");
                            }
                        }
                    }
                }
                string str3 = builder2.ToString();
                str3 = str3.Substring(0, str3.LastIndexOf("\t"));
                builder.Append(str3);
            }
            return builder.ToString();
        }

        public static T XmlToObject<T>(List<XElement> xList, T obj) where T: new()
        {
            Type type = typeof(T);
            PropertyInfo[] properties = null;
            properties = type.GetProperties();
            if (obj == null)
            {
                obj = new T();
            }
            foreach (XElement element in xList)
            {
                foreach (PropertyInfo info in properties)
                {
                    if (string.Equals(info.Name, element.Name.LocalName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Type propertyType = info.PropertyType;
                        if (propertyType.IsGenericType && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            if (string.IsNullOrEmpty(element.Value))
                            {
                                info.SetValue(obj, null, null);
                            }
                            else if (element.HasElements)
                            {
                                info.SetValue(obj, Convert.ChangeType(element.ToString(), Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType), null);
                            }
                            else
                            {
                                info.SetValue(obj, Convert.ChangeType(element.Value, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType), null);
                            }
                        }
                        else
                        {
                            info.SetValue(obj, Convert.ChangeType(element.Value, propertyType), null);
                        }
                    }
                }
            }
            return obj;
        }

        /// <summary>
        /// 反序列化XML字符串
        /// </summary>
        /// <param name="type">typeof(T)</param>
        /// <param name="xml">XML字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                System.Xml.Serialization.XmlSerializer xmldes = new System.Xml.Serialization.XmlSerializer(type);
                return xmldes.Deserialize(sr);
            }
        }

        /// <summary>
        /// 序列化字符串
        /// </summary>
        /// <param name="type">typeof(T)</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            //序列化对象  
            xml.Serialize(Stream, obj);
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }  
    }
}

