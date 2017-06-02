using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercatsListUpdateResponse.
    /// </summary>
    public class SellercatsListUpdateResponse : TopResponse
    {
        /// <summary>
        /// 返回sellercat数据结构中的：cid,modified
        /// </summary>
        [XmlElement("seller_cat")]
        public Top.Api.Domain.SellerCat SellerCat { get; set; }

    }
}
