using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rooms.increment
    /// </summary>
    public class XhotelRoomsIncrementRequest : BaseTopRequest<Top.Api.Response.XhotelRoomsIncrementResponse>
    {
        /// <summary>
        /// 批量全量推送房型共享库存,一次最多修改30个房型。json encode。示例：[{"out_rid":"hotel1_roomtype22","vendor":"","allotment_start_Time":"","allotment_end_time":"","superbook_start_time":"","superbook_end_time":"","roomQuota":[{"date":2010-01-28,"quota":10,"al_quota":2,"sp_quota":3}]}] 其中al_quota为保留房库存，sp_quota为超预定库存，quota为物理库存。al_quota和sp_quota需要向运营申请权限才可维护。allotment_start_Time和allotment_end_time为保留房库存开始和截止时间；superbook_start_time和superbook_end_time为超预定库存开始和截止时间。
        /// </summary>
        public string RoomQuotaMap { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rooms.increment";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("room_quota_map", this.RoomQuotaMap);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("room_quota_map", this.RoomQuotaMap);
        }

        #endregion
    }
}
