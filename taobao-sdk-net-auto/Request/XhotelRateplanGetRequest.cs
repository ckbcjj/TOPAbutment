using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rateplan.get
    /// </summary>
    public class XhotelRateplanGetRequest : BaseTopRequest<Top.Api.Response.XhotelRateplanGetResponse>
    {
        /// <summary>
        /// 卖家自己系统的Code，简称RateCode
        /// </summary>
        public string RateplanCode { get; set; }

        /// <summary>
        /// 废弃，使用rateplan_code
        /// </summary>
        public Nullable<long> Rpid { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rateplan.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("rateplan_code", this.RateplanCode);
            parameters.Add("rpid", this.Rpid);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("rateplan_code", this.RateplanCode, 32);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
