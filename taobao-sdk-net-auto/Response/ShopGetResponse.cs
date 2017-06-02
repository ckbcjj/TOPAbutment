using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// ShopGetResponse.
    /// </summary>
    public class ShopGetResponse : TopResponse
    {
        /// <summary>
        /// 店铺信息
        /// </summary>
        [XmlElement("shop")]
        public Top.Api.Domain.Shop Shop { get; set; }

    }
}
