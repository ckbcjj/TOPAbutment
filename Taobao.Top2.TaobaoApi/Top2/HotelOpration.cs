using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.Tool;
using Taobao.Top2.Entity.TaobaoEntity;
using Top.Api.Request;

namespace Taobao.Top2.TaobaoApi.Top2
{
    public  class HotelOpration
    {
        public TaobaoHotel HotelUpLoad(TaobaoHotel hotel)
        {
            XhotelAddRequest req = new XhotelAddRequest();
            req.Address = hotel.address;
            try
            {
                req.Province = long.Parse(hotel.province);
                req.City = long.Parse(hotel.city); 
            }
            catch
            {
                return null;
            }          
            req.Country = "China";
            req.District = 0L;
            req.Domestic = 0L;
            req.Name = hotel.name;
            req.OuterId = hotel.qmg_HotelId.ToString();
            var va = TopConfig.Execute(req);
            if (va.IsError == false)
            {
                try
                {
                    XmlHelper.XmlToObject(
                        XDocument.Parse(va.Body).Element("xhotel_add_response").Elements("xhotel").Elements().ToList(),
                        hotel);
                    return hotel;
                }
                catch
                {
                    throw new ArgumentException(va.Body);
                }
            }
            throw new ArgumentException(va.Body);
        }
        
    }
}
    