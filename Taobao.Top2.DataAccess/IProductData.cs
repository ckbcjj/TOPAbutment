using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess
{
    public interface IProductData
    {
        DataTable GetTaobaoRoomType(List<string> hotels);

        /// <summary>
        /// 获取需要下线的数据
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="hotelIds"></param>
        /// <returns></returns>
        DataTable GetDownList(List<int> rooms, int[] hotelIds = null);

        /// <summary>
        /// 获取全量更新数据
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="hotelIds"></param>
        /// <returns></returns>
        DataTable GetUpdateList(int[] hotelIds = null);
        DataTable GetRoomStatus(string roomid, double lijianRate, double zengfuRate);

        /// <summary>
        /// 更新rpid
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <param name="rpid"></param>
        void UpdateRpId(int hotelid, int roomid, string rpid);

        /// <summary>
        /// 获取酒店房型信息
        /// </summary>
        /// <param name="roomids"></param>
        /// <returns></returns>
        DataTable GetRoomInfoByRoom(int roomid);

        DataTable GetTaobaoRoomTypeByRid(string rid);

        bool UpdataProductHis(TaobaoProduct product);

        /// <summary>
        /// 更新gid
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <param name="gid"></param>
        void UpdateGId(string hid, string rid, long rpid, string gid);

        /// <summary>
        /// 更新gid
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <param name="gid"></param>
        void UpdateGId(int hotelid, int roomid, string gid);

        DataTable GetIncrPrice(DateTime begintime, DateTime endtime);

        void CleanOffLineData(string rpid);

        DataTable GetRpidListByHotelId(int hotelid);

        DataTable GetRpidListByRoomId(int roomid);

        void UpdateProductStatusByHotelId(int hotelid, bool online);

        void UpdateProductStatusByRoomId(int roomid, bool online);
    }
}
