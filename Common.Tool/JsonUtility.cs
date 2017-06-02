using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Common.Tool
{
    public class JsonUtility
    {
        private static JsonUtility _instance = new JsonUtility();

        public T JsonToObject<T>(string json)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return (T) serializer.Deserialize(json, typeof(T));
        }

        public IList<T> JsonToObjectList<T>(string json)
        {
            json = json.Replace("}]}", "");
            int startIndex = json.IndexOf(":[{") + 3;
            string input = json.Substring(startIndex);
            string[] strArray = new Regex("},{").Split(input);
            if (input.Contains("\":[]}"))
            {
                return null;
            }
            List<T> list = new List<T>();
            foreach (string str2 in strArray)
            {
                string str3 = "{" + str2 + "}";
                list.Add(this.JsonToObject<T>(str3));
            }
            return list;
        }

        public string ObjectListToJson<T>(IList<T> objectList)
        {
            return this.ObjectListToJson<T>(objectList, "");
        }

        public string ObjectListToJson<T>(IList<T> objectList, string className)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{");
            className = string.IsNullOrEmpty(className) ? objectList[0].GetType().Name : className;
            builder.Append("\"" + className + "\":[");
            for (int i = 0; i < objectList.Count; i++)
            {
                T local = objectList[i];
                if (i > 0)
                {
                    builder.Append(",");
                }
                builder.Append(this.ObjectToJson(local));
            }
            builder.Append("]}");
            return builder.ToString();
        }

        public string ObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ObjectToJson(object obj, Action action)
        {
            action();
            return JsonConvert.SerializeObject(obj);
        }

        public static JsonUtility Instance
        {
            get
            {
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public delegate void ChCoding<T>(T obj);
    }
}

