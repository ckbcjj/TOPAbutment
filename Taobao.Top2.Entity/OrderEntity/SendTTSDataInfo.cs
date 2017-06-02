using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taobao.Top2.Entity.OrderEntity
{
    public class SendTTSDataInfo
    {
        public int orderid { get; set; }
        public int type { get; set; }
        public int status { get; set; }
        public int statuscode { get; set; }
        public string ext0 { get; set; }
    }
}
