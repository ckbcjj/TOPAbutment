using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.multiplerate.get
    /// </summary>
    public class XhotelMultiplerateGetRequest : BaseTopRequest<Top.Api.Response.XhotelMultiplerateGetResponse>
    {
        /// <summary>
        /// 废弃，使用out_rid
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 连住天数(范围1~10)
        /// </summary>
        public Nullable<long> Nod { get; set; }

        /// <summary>
        /// 入住人数(范围1~10)
        /// </summary>
        public Nullable<long> Nop { get; set; }

        /// <summary>
        /// 卖家的房型code
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 卖家的房价code
        /// </summary>
        public string RatePlanCode { get; set; }

        /// <summary>
        /// 废弃，使用rate_plan_code
        /// </summary>
        public Nullable<long> RatePlanId { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.multiplerate.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gid", this.Gid);
            parameters.Add("nod", this.Nod);
            parameters.Add("nop", this.Nop);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("rate_plan_code", this.RatePlanCode);
            parameters.Add("rate_plan_id", this.RatePlanId);
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
