using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess
{
    public interface IRoomData
    {
        DataTable GetRoomInfoById(string roomId);

        /// <summary>
        /// 保存上传成功的酒店房型信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        bool SaveTaobaoRoom(TaobaoRoom room);
        DataTable GetRoomInfoByHotel(string hotel);

        void SaveToRoomType(TaobaoRoom room);
        DataTable GetRoomInfoByHotelId(int hotelid);
        /// <summary>
        /// 清理不存在的房型的数据
        /// </summary>
        /// <param name="hid"></param>
        void CleanOffLineData(string rid);

        DataTable GetRoomTypeStatus();

        void UpdateTaobaoRoomLogStatus(int roomid);
    }
}
