using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelOrderUpdateResponse.
    /// </summary>
    public class XhotelOrderUpdateResponse : TopResponse
    {
        /// <summary>
        /// 返回提示信息
        /// </summary>
        [XmlElement("result")]
        public string Result { get; set; }

    }
}
