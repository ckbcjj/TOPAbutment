using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Common.Tool;
using System.Threading;

namespace Taobao.Top2.Application.Common
{
    public class TopConfig
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly string Url = "http://gw.api.taobao.com/router/rest?";

        public static string Appkey = "23757801";

        public static string AppSecret = "37999e3f0dde002bc14849a222ff8c08";

        public static string SessionKey = "6102b10b59441755a713a6f6c3bb172806de18756c50c293171644394";

        public static ITopClient GetClient()
        {
            return new DefaultTopClient(Url, Appkey, AppSecret);
        }

        public static T Execute<T>(ITopRequest<T> request) where T : TopResponse
        {
            var top = GetClient();
            T res = null;
            int i = 0;
            while (i < 3)
            {
                res = top.Execute(request, SessionKey);
                if (res.IsError && (res.ErrCode == "15" && res.SubErrCode == "isp.top-remote-connection-timeout") || (res.ErrCode == "7" && res.SubErrCode == "accesscontrol.limited-by-api-access-count"))
                {
                    Thread.Sleep(3000);
                    continue;
                }
                else
                {
                    return res;
                }
            }
            log.Warning("接口请求超时三次或暂时调用频率限制。请求:" + request.GetParameters());
            return res;
        }

        public static T Execute<T>(ITopRequest<T> request, ITopClient top) where T : TopResponse
        {
            if (top == null)
            {
                top = GetClient();
            }
            return top.Execute(request, SessionKey);
        }
    }
}
