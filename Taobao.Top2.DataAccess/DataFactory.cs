using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.DataAccess.Implement;

namespace Taobao.Top2.DataAccess
{
    public static class DataFactory
    {
        public static IHotelData CreateHotelData()
        {
            return new HotelData();
        }

        public static IRoomData CreateRoomData()
        {
            return new RoomData();
        }

        public static IOrderData CreateOrderData()
        {
            return new OrderData();
        }

        public static IProductData CreateProductData()
        {
            return new ProductData();
        }
    }
}
