using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercenterUserPermissionsGetResponse.
    /// </summary>
    public class SellercenterUserPermissionsGetResponse : TopResponse
    {
        /// <summary>
        /// 权限列表
        /// </summary>
        [XmlArray("permissions")]
        [XmlArrayItem("permission")]
        public List<Top.Api.Domain.Permission> Permissions { get; set; }

    }
}
