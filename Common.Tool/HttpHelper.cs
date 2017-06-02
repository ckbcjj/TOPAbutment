using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Common.Tool
{
    public class HttpHelper
    {
        public static HttpWebRequest CreatHttpWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Timeout = 0xea60;
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            return request;
        }

        public static string Get(HttpWebRequest req)
        {
            return Send(req, null);
        }

        public static string Get(string url)
        {
            return Send(CreatHttpWebRequest(url), null);
        }

        public static string Post(HttpWebRequest req, string postData)
        {
            return Send(req, postData);
        }

        public static string Post(string url, string postData)
        {
            return Send(CreatHttpWebRequest(url), postData);
        }

        public static string Send(HttpWebRequest req, string postData = null)
        {
            string str2;
            try
            {
                if (!string.IsNullOrEmpty(postData))
                {
                    req.Method = "POST";
                    byte[] bytes = Encoding.GetEncoding("utf-8").GetBytes(postData);
                    req.ContentType = "application/text";
                    req.ContentLength = bytes.Length;
                    Stream requestStream = req.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                else
                {
                    req.Method = "GET";
                }
                using (HttpWebResponse response = (HttpWebResponse) req.GetResponse())
                {
                    using (Stream stream2 = response.GetResponseStream())
                    {
                        StreamReader reader;
                        if ("gzip".Equals(response.ContentEncoding, StringComparison.CurrentCultureIgnoreCase))
                        {
                            reader = new StreamReader(new GZipStream(stream2, CompressionMode.Decompress), Encoding.UTF8);
                        }
                        else
                        {
                            reader = new StreamReader(stream2, Encoding.UTF8);
                        }
                        string str = null;
                        str = reader.ReadToEnd();
                        reader.Close();
                        str2 = str;
                    }
                }
            }
            catch (Exception exception)
            {
                str2 = "HTTP Exception: \n" + exception.Message;
            }
            return str2;
        }
    }
}

