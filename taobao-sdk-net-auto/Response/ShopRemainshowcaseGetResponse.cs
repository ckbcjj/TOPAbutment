using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// ShopRemainshowcaseGetResponse.
    /// </summary>
    public class ShopRemainshowcaseGetResponse : TopResponse
    {
        /// <summary>
        /// 支持返回剩余橱窗数量，已用橱窗数量，总橱窗数量
        /// </summary>
        [XmlElement("shop")]
        public RemainCountShopDomain Shop { get; set; }

	/// <summary>
/// RemainCountShopDomain Data Structure.
/// </summary>
[Serializable]
public class RemainCountShopDomain : TopObject
{
	        /// <summary>
	        /// 总橱窗数量，对于C卖家返回总橱窗数，对于B卖家返回0（只有taobao.shop.remainshowcase.get可以返回）
	        /// </summary>
	        [XmlElement("all_count")]
	        public long AllCount { get; set; }
	
	        /// <summary>
	        /// 已用的橱窗数量，对于C卖家返回已使用的橱窗数，对于B卖家返回-1（只有taobao.shop.remainshowcase.get可以返回）
	        /// </summary>
	        [XmlElement("remain_count")]
	        public long RemainCount { get; set; }
	
	        /// <summary>
	        /// 剩余橱窗数量，对于C卖家返回剩余橱窗数，对于B卖家返回-1（只有taobao.shop.remainshowcase.get可以返回）
	        /// </summary>
	        [XmlElement("used_count")]
	        public long UsedCount { get; set; }
}

    }
}
