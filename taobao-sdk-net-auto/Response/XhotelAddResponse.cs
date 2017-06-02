using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelAddResponse.
    /// </summary>
    public class XhotelAddResponse : TopResponse
    {
        /// <summary>
        /// 酒店信息
        /// </summary>
        [XmlElement("xhotel")]
        public Top.Api.Domain.XHotel Xhotel { get; set; }

    }
}
