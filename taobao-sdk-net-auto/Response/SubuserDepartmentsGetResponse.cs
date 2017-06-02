using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// SubuserDepartmentsGetResponse.
    /// </summary>
    public class SubuserDepartmentsGetResponse : TopResponse
    {
        /// <summary>
        /// 部门信息
        /// </summary>
        [XmlArray("departments")]
        [XmlArrayItem("department")]
        public List<Top.Api.Domain.Department> Departments { get; set; }

    }
}
