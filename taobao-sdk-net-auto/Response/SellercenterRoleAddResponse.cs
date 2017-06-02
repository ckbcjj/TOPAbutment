using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercenterRoleAddResponse.
    /// </summary>
    public class SellercenterRoleAddResponse : TopResponse
    {
        /// <summary>
        /// 子账号角色
        /// </summary>
        [XmlElement("role")]
        public Top.Api.Domain.Role Role { get; set; }

    }
}
