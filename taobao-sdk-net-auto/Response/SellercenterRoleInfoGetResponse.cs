using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercenterRoleInfoGetResponse.
    /// </summary>
    public class SellercenterRoleInfoGetResponse : TopResponse
    {
        /// <summary>
        /// 角色具体信息
        /// </summary>
        [XmlElement("role")]
        public Top.Api.Domain.Role Role { get; set; }

    }
}
