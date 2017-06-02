using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// XhotelRoomsIdsGetResponse.
    /// </summary>
    public class XhotelRoomsIdsGetResponse : TopResponse
    {
        /// <summary>
        /// 根据条件所查询的所有结果的总数量
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

        /// <summary>
        /// 宝贝相关的ID集合
        /// </summary>
        [XmlArray("xroom_ids")]
        [XmlArrayItem("x_room_ids")]
        public List<Top.Api.Domain.XRoomIds> XroomIds { get; set; }

    }
}
