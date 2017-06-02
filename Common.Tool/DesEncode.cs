using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common.Tool
{
    public class DesEncode
    {
        public static string DecryptDES(string decryptString, string decryptKey, string iv)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] buffer = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.UTF8.GetString(stream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }

        public static string EncryptDES(string encryptString, string encryptKey, string iv)
        {
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Encoding.UTF8.GetBytes(iv);
                byte[] buffer = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider {
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(bytes, rgbIV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                stream.ToArray();
                return Convert.ToBase64String(stream.ToArray());
            }
            catch
            {
                return encryptString;
            }
        }

        public static string MgDec(string code, string key)
        {
            List<byte> list = new List<byte>();
            for (int i = 0; i < (code.Length - 1); i += 2)
            {
                byte item = byte.Parse(code[i].ToString() + code[i + 1], NumberStyles.HexNumber);
                list.Add(item);
            }
            return DecryptDES(Convert.ToBase64String(list.ToArray()), key, key);
        }

        public static string MgEnc(string code, string key, string time)
        {
            code = time + "#" + code;
            string s = EncryptDES(code, key, key);
            StringBuilder builder = new StringBuilder();
            byte[] buffer = Convert.FromBase64String(s);
            for (int i = 0; i < buffer.Length; i++)
            {
                builder.Append(buffer[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}

