using System.Collections.Generic;
using System.Data;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.Application
{
    public interface IHotelUpload
    {
        List<TaobaoHotel> UpLoad(List<int> hotelId);
        DataTable GetHotelsByCondition(Dictionary<string, object> dic);
        DataRow CheckTaobaoHotelByHid(string hid);
        DataRow CheckQmgHotelByHotelid(int hotelid);
        void SynHotelsStatus();
    }
}
