using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercatsListGetResponse.
    /// </summary>
    public class SellercatsListGetResponse : TopResponse
    {
        /// <summary>
        /// 卖家自定义类目
        /// </summary>
        [XmlArray("seller_cats")]
        [XmlArrayItem("seller_cat")]
        public List<Top.Api.Domain.SellerCat> SellerCats { get; set; }

    }
}
