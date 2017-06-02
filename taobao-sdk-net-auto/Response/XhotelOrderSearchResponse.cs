using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelOrderSearchResponse.
    /// </summary>
    public class XhotelOrderSearchResponse : TopResponse
    {
        /// <summary>
        /// 订单信息
        /// </summary>
        [XmlArray("hotel_orders")]
        [XmlArrayItem("x_hotel_order")]
        public List<Top.Api.Domain.XHotelOrder> HotelOrders { get; set; }

        /// <summary>
        /// 符合条件的结果总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
