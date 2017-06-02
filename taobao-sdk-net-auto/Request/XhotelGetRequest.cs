using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.get
    /// </summary>
    public class XhotelGetRequest : BaseTopRequest<Top.Api.Response.XhotelGetResponse>
    {
        /// <summary>
        /// 废弃，请使用outer_id
        /// </summary>
        public Nullable<long> Hid { get; set; }

        /// <summary>
        /// 卖家系统中的酒店ID。
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// 系统商，一般不用填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("hid", this.Hid);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("outer_id", this.OuterId, 64);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
