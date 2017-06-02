using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.TaobaoApi
{
    public interface IRoomOpration
    {
        TaobaoRoom GetRoomTypeByRid(string rid);
        TaobaoRoom UpLoadTaobaoRoomInfo(int roomid, int hotelid);

    }
}
