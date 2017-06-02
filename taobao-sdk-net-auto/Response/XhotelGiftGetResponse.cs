using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelGiftGetResponse.
    /// </summary>
    public class XhotelGiftGetResponse : TopResponse
    {
        /// <summary>
        /// 礼包信息
        /// </summary>
        [XmlElement("gift")]
        public Top.Api.Domain.Gift Gift { get; set; }

    }
}
