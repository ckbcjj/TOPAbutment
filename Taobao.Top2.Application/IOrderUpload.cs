using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.OrderEntity;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.Application
{
    public interface IOrderUpload
    {
        /// <summary>
        /// 获取淘宝订单信息
        /// </summary>
        /// <returns></returns>
        List<HotelOrderInfo> GetTaobaoOrders();

        /// <summary>
        /// 生成新订单(返回订单列表)
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns></returns>
        List<HotelOrderInfo> AddNewOrder(List<HotelOrderInfo> hotelOrderInfoList);

        /// <summary>
        /// 生成房型数据并更新订单数据
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns></returns>
        List<HotelOrderInfo> AddOrderRoom(List<HotelOrderInfo> hotelOrderInfoList);

        /// <summary>
        /// 酒店订单发货
        /// </summary>
        /// <param name="hotelOrderList"></param>
        void SetTaobaoOrderStart(List<HotelOrderInfo> hotelOrderList);
    }
}
