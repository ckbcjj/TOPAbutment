using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common.Tool;
using Taobao.Top2.Application;
using Taobao.Top2.Application.Implement;

namespace Taobao.Top2.Api
{
    public class TaobaoUpLoadController : ApiController
    {
        private IProductUpload upload = new ProductUpload();
        // GET api/<controller>
        public void Get()
        {
            upload.PriceUpdate(null, true, null);
        }

        public void Get(string id)
        {
            if (id != null && id.ToLower().Contains("incr"))
            {
                upload.UpdateProductsIncr(5);
            }
            else
            {
                List<int> list = id.NoSqlHackStringToIntArr();
                upload.PriceUpdate(list, true, null);
            }
        }
    }
}