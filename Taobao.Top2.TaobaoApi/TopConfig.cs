using Top.Api;
using Top.Api.Request;

namespace Taobao.Top2.TaobaoApi
{
    internal class TopConfig
    {
        public static readonly string Url = "http://gw.api.taobao.com/router/rest?";

        public const string Appkey = "23757801";
        public const string AppSecret = "37999e3f0dde002bc14849a222ff8c08";
        public const string SessionKey = "6102b10b59441755a713a6f6c3bb172806de18756c50c293171644394";

        public static ITopClient GetClient()
        {
            return new DefaultTopClient(Url, Appkey, AppSecret);
        }

        public static T Execute<T>(ITopRequest<T> request) where T : TopResponse
        {
            var top = GetClient();
            return top.Execute(request, SessionKey);
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
    