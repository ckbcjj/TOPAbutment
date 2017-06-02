using System.Security.Cryptography;
using System.Text;

namespace Common.Tool
{
    public class DataHelper
    {
        public static string Md5(string input)
        {
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(input);
            byte[] buffer2 = md.ComputeHash(bytes);
            StringBuilder builder = new StringBuilder();
            foreach (byte num in buffer2)
            {
                builder.Append(num.ToString("X2"));
            }
            return builder.ToString();
        }
    }
}

