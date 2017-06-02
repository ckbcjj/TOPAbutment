using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SellercenterSubuserPermissionsRolesGetResponse.
    /// </summary>
    public class SellercenterSubuserPermissionsRolesGetResponse : TopResponse
    {
        /// <summary>
        /// 子账号被所拥有的权限
        /// </summary>
        [XmlElement("subuser_permission")]
        public Top.Api.Domain.SubUserPermission SubuserPermission { get; set; }

    }
}
