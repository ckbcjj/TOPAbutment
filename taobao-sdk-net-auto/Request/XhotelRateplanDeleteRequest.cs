using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rateplan.delete
    /// </summary>
    public class XhotelRateplanDeleteRequest : BaseTopRequest<Top.Api.Response.XhotelRateplanDeleteResponse>
    {
        /// <summary>
        /// 商家价格政策编码
        /// </summary>
        public string RateplanCode { get; set; }

        /// <summary>
        /// ratepland标识
        /// </summary>
        public Nullable<long> RpId { get; set; }

        /// <summary>
        /// 系统商，一般不用填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rateplan.delete";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("rateplan_code", this.RateplanCode);
            parameters.Add("rp_id", this.RpId);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
        }

        #endregion
    }
}
