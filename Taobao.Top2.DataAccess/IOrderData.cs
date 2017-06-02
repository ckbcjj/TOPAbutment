using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.OrderEntity;
using System.Data;

namespace Taobao.Top2.DataAccess
{
    public interface IOrderData
    {
        /// <summary>
        /// 获取上传淘宝酒店信息
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        DataTable GetHotelById(List<string> hid);

        /// <summary>
        /// 获取在售酒店信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns></returns>
        DataTable GetSaleHotelById(List<string> hotelid);

        /// <summary>
        /// 获取上传淘宝房型信息
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        DataTable GetRoomsById(List<string> rpid);


        /// <summary>
        /// 获取在售房型信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        DataTable GetSaleRoomById(List<string> hotelid, List<string> roomid);

        /// <summary>
        /// 生成新订单
        /// </summary>
        /// <param name="hotelOrderInfo">订单实体</param>
        /// <returns>返回订单号</returns>
        int AddNewOrder(HotelOrderInfo hotelOrderInfo);

        /// <summary>
        /// 生成房型数据并更新订单数据
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns></returns>
        HotelOrderInfo AddOrderRoom(HotelOrderInfo hotelOrderInfo);


        DataTable GetBatchRoomList(int hotelId, int room);
    }
}
