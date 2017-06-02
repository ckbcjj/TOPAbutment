using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelMultiplerateGetResponse.
    /// </summary>
    public class XhotelMultiplerateGetResponse : TopResponse
    {
        /// <summary>
        /// 复杂价格返回结果类
        /// </summary>
        [XmlArray("rates")]
        [XmlArrayItem("multiple_rate")]
        public List<Top.Api.Domain.MultipleRate> Rates { get; set; }

    }
}
