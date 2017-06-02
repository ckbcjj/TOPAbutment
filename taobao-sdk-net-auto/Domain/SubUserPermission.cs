using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// SubUserPermission Data Structure.
    /// </summary>
    [Serializable]
    public class SubUserPermission : TopObject
    {
        /// <summary>
        /// 子账号被直接赋予的权限点列表
        /// </summary>
        [XmlArray("permissions")]
        [XmlArrayItem("permission")]
        public List<Top.Api.Domain.Permission> Permissions { get; set; }

        /// <summary>
        /// 子账号被赋予的角色信息(Role)列表。列表中的角色对象只有role_id，role_name，permissions信息
        /// </summary>
        [XmlArray("roles")]
        [XmlArrayItem("role")]
        public List<Top.Api.Domain.Role> Roles { get; set; }
    }
}
