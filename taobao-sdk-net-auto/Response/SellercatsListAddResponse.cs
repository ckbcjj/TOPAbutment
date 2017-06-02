using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercatsListAddResponse.
    /// </summary>
    public class SellercatsListAddResponse : TopResponse
    {
        /// <summary>
        /// 返回seller_cat数据结构中的：cid,created
        /// </summary>
        [XmlElement("seller_cat")]
        public Top.Api.Domain.SellerCat SellerCat { get; set; }

    }
}
