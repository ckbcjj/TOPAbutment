using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelGetResponse.
    /// </summary>
    public class XhotelGetResponse : TopResponse
    {
        /// <summary>
        /// 查询得到的hotel
        /// </summary>
        [XmlElement("xhotel")]
        public Top.Api.Domain.XHotel Xhotel { get; set; }

    }
}
