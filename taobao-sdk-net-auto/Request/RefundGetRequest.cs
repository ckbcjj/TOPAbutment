using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.refund.get
    /// </summary>
    public class RefundGetRequest : BaseTopRequest<Top.Api.Response.RefundGetResponse>
    {
        /// <summary>
        /// 需要返回的字段。目前支持有：refund_id, alipay_no, tid, oid, buyer_nick, seller_nick, total_fee, status, created, refund_fee, good_status, has_good_return, payment, reason, desc, num_iid, title, price, num, good_return_time, company_name, sid, address, shipping_type, refund_remind_timeout, refund_phase, refund_version, operation_contraint, attribute, outer_id, sku
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 退款单号
        /// </summary>
        public Nullable<long> RefundId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.refund.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("refund_id", this.RefundId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateMaxListSize("fields", this.Fields, 100);
            RequestValidator.ValidateRequired("refund_id", this.RefundId);
            RequestValidator.ValidateMaxValue("refund_id", this.RefundId, 9223372036854775807);
            RequestValidator.ValidateMinValue("refund_id", this.RefundId, 1);
        }

        #endregion
    }
}
