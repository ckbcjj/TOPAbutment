using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelUpdateResponse.
    /// </summary>
    public class XhotelUpdateResponse : TopResponse
    {
        /// <summary>
        /// 酒店信息
        /// </summary>
        [XmlElement("xhotel")]
        public Top.Api.Domain.XHotel Xhotel { get; set; }

    }
}
