using System.Data.SqlTypes;

namespace Taobao.Top2.Entity.TaobaoEntity
{
    public class TaobaoProduct
    {
        public string createTime { get; set; }

        public string gid { get; set; }

        public string hid { get; set; }

        public string inventory_price { get; set; }

        public string qmg_hotelid { get; set; }

        public string qmg_roomid { get; set; }

        public string Response { get; set; }

        public string rid { get; set; }

        public string rpid { get; set; }

        public string rateplan_code { get; set; }

        public int status { get; set; }

        public int zengfu { get; set; }

        public int lijian { get; set; }
        public SqlDateTime modified_time { get; set; }
    }
}

