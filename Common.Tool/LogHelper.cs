using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Configuration;
using log4net;

namespace Common.Tool
{
    public class LogHelper
    {
        private static readonly string _logPatch = WebConfigurationManager.AppSettings["LogPath"];
        private static Task _logTask;
        private static readonly ConcurrentQueue<string> concurrent = new ConcurrentQueue<string>();
        private static readonly string logFomat = "start====={0}=====:\r\n\t{1}\r\nend====={0}=====\r\n";
        private static readonly object taskAnysObject = new object();

        private static void createTask()
        {
            if (_logTask == null)
            {
                lock (taskAnysObject)
                {
                    if (_logTask == null)
                    {
                        _logTask = new Task(new Action(LogHelper.Log));
                        _logTask.Start();
                    }
                }
            }
        }

        private static string GetOrCreateDir(string dirName, string path)
        {
            if (!path.EndsWith(@"\"))
            {
                path = path + @"\";
            }
            path = path + dirName;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        private static string GetOrCreateFile(string filename, string dir)
        {
            if (!dir.EndsWith(@"\"))
            {
                dir = dir + @"\";
            }
            string path = dir + filename;
            if (!File.Exists(path))
            {
                File.AppendAllText(path, "");
            }
            return path;
        }

        private static void Log()
        {
            DateTime minValue = DateTime.MinValue;
            string path = null;
            while (true)
            {
                if (minValue == DateTime.Now.Date)
                {
                    string str2;
                    if ((concurrent.Count > 0) && concurrent.TryDequeue(out str2))
                    {
                        File.AppendAllText(path, string.Format(logFomat, DateTime.Now, str2), Encoding.GetEncoding("gbk"));
                    }
                }
                else
                {
                    minValue = DateTime.Now.Date;
                    string dirName = minValue.Month.ToString();
                    string orCreateDir = GetOrCreateDir(dirName, GetOrCreateDir(minValue.Year.ToString(), _logPatch));
                    path = GetOrCreateFile(minValue.Date.ToString("yyyy-MM-dd") + ".txt", orCreateDir);
                }
                Thread.Sleep(0x3e8);
            }
        }

        public static void Logs(string logInfo)
        {
            createTask();
            concurrent.Enqueue(logInfo);
        }
    }

    public class Log4Helper
    {
        private ILog logger;

        public Log4Helper(ILog log)
        {
            this.logger = log;
        }

        public void Info(object message)
        {
            this.logger.Info(message);
            Console.WriteLine(message + "  " + DateTime.Now.ToShortTimeString());
        }

        public void Info_Log(object message)
        {
            this.logger.Info(message);
        }

        public void Info(object message, Exception e)
        {
            this.logger.Info(message, e);
        }

        public void Debug(object message)
        {
            this.logger.Debug(message);
        }

        public void Debug(object message, Exception e)
        {
            this.logger.Debug(message, e);
        }

        public void Warning(object message)
        {
            this.logger.Warn(message);
        }

        public void Warning(object message, Exception e)
        {
            this.logger.Warn(message, e);
        }

        public void Error(object message)
        {
            this.logger.Error(message);
        }

        public void Error(object message, Exception e)
        {
            this.logger.Error(message, e);
        }

        public void Fatal(object message)
        {
            this.logger.Fatal(message);
        }

        public void Fatal(object message, Exception e)
        {
            this.logger.Fatal(message, e);
        }
    }

    public class Log4Factory
    {
        static Log4Factory()
        {
        }

        public static Log4Helper GetLogger(Type type)
        {
            return new Log4Helper(LogManager.GetLogger(type));
        }

        public static Log4Helper GetLogger(string str)
        {
            return new Log4Helper(LogManager.GetLogger(str));
        }
    }
}

