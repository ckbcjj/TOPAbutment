using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taobao.Top2.Entity.TaobaoEntity
{
    public class TaobaoRoom : TaobaoRoomBase
    {
        public string hotelname { get; set; }

        public string roomname { get; set; }

        public string statusEx { get; set; }
        public string outHid { get; set; }
    }
}
