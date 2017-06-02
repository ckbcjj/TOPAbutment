using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.Application
{
    public interface IProductUpload
    {
        void UpdataProductHis(TaobaoProduct rooms);
        string GetGidByRpid(string rpid);

        /// <summary>
        /// 全量更新价格
        /// </summary>
        /// <param name="hotelIds"></param>
        void PriceUpdate(int[] hotelIds = null);
        void SetLijian(int lijian, int[] rooms);

        void UpdateProductsIncr(DateTime begintime, DateTime endtime);

        void UpLoadByRoom(TaobaoRoom room);

        void ProductStatusUpateByHotelid(List<int> hotelidList, bool isup);

        void DeleteHotel(List<long> hidList);
    }
}
