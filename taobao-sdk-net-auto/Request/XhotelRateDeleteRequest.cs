using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rate.delete
    /// </summary>
    public class XhotelRateDeleteRequest : BaseTopRequest<Top.Api.Response.XhotelRateDeleteResponse>
    {
        /// <summary>
        /// 房型id
        /// </summary>
        public string Gid { get; set; }

        /// <summary>
        /// 商家房型ID
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// rateId
        /// </summary>
        public Nullable<long> RateId { get; set; }

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
            return "taobao.xhotel.rate.delete";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gid", this.Gid);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("rate_id", this.RateId);
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
