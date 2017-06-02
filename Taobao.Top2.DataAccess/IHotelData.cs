using System.Collections.Generic;
using System.Data;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess
{
    public interface IHotelData
    {
        DataTable GetHotelById(string hotelid);

        DataTable GetHotelById(List<int> hotelid);

        /// <summary>
        /// 保存酒店信息
        /// </summary>
        /// <param name="taobao">酒店实体</param>
        /// <returns></returns>
        bool SaveHotels(TaobaoHotel taobao);

        DataTable GetHotelsByCondition(Dictionary<string, object> dic);
        /// <summary>
        /// 判断酒店是否上传
        /// </summary>
        /// <param name="qmgHotelId"></param>
        /// <returns></returns>
        string GetTaobaoHotel(int qmgHotelId);
        DataTable GetHidByHotelId(int[] hotelid);

        /// <summary>
        /// 查询表Taobao_New_Hotel
        /// </summary>
        /// <param name="hid"></param>
        /// <returns></returns>
        DataTable TaoBaoHotelSearch(string hid);

        /// <summary>
        /// 查询表Taobao_new_Hotel
        /// </summary>
        DataTable TaoBaoHotelSearch(int hotelid);

        void UpDateTaobaoHotel(TaobaoHotel taobaoHotel);

        /// <summary>
        /// 清理所有不存在的酒店的数据
        /// </summary>
        /// <param name="hid"></param>
        void CleanOffLineData(string hid);

        /// <summary>
        /// 清理不存在的酒店的数据(针对多价格)
        /// </summary>
        void CleanOffLineDataByHotelId(int hotelid);

        DataTable GetHotelStatus();

        void UpdateTaobaoHotelLogStatus(int hotelid);

        DataTable GetTaoBaoHotel(int hotelid);
    }
}
