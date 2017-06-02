using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.baseinfo.room.get
    /// </summary>
    public class XhotelBaseinfoRoomGetRequest : BaseTopRequest<Top.Api.Response.XhotelBaseinfoRoomGetResponse>
    {
        /// <summary>
        /// 是否需要房价基本信息（false为不需要），默认为需要
        /// </summary>
        public Nullable<bool> IsNeedRatePlan { get; set; }

        /// <summary>
        /// 卖家系统中的酒店ID。
        /// </summary>
        public string OutHid { get; set; }

        /// <summary>
        /// 用于标示该酒店发布的渠道信息
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.baseinfo.room.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("is_need_rate_plan", this.IsNeedRatePlan);
            parameters.Add("out_hid", this.OutHid);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("out_hid", this.OutHid);
            RequestValidator.ValidateMaxLength("out_hid", this.OutHid, 20);
        }

        #endregion
    }
}
