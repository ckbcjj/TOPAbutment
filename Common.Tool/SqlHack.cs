using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Tool
{
    public static class SqlHack
    {
        public static string NoSqlHack(this string Inner)
        {
            if (!string.IsNullOrEmpty(Inner))
            {
                //特殊的字符
                Inner = Inner.Replace("'", "''");
                return Inner;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string NoSqlHackIn(this string Inner)
        {
            string[] arr = Inner.Split(',');
            if (arr.Length > 0)
            {
                int temp = 0;
                StringBuilder sb = new StringBuilder();
                foreach (var a in arr)
                {
                    if (int.TryParse(a, out temp))
                    {
                        sb.Append(a);
                        sb.Append(",");
                    }
                }
                if (sb.Length > 0)
                {
                    return sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            return null;
        }

        public static List<int> NoSqlHackStringToIntArr(this string Inner)
        {
            string[] arr = Inner.Split(',');
            List<int> list = new List<int>();
            if (arr.Length > 0)
            {
                int temp = 0;
                foreach (var a in arr)
                {
                    if (int.TryParse(a, out temp))
                    {
                        list.Add(temp);
                    }
                }
            }
            return list;
        }
    }
}
