using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SubuserInfoUpdateResponse.
    /// </summary>
    public class SubuserInfoUpdateResponse : TopResponse
    {
        /// <summary>
        /// 操作是否成功 true:操作成功; false:操作失败
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

    }
}
