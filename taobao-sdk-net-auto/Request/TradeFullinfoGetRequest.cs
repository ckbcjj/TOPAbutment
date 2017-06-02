using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trade.fullinfo.get
    /// </summary>
    public class TradeFullinfoGetRequest : BaseTopRequest<Top.Api.Response.TradeFullinfoGetResponse>
    {
        /// <summary>
        /// 需要返回的字段列表，多个字段用半角逗号分隔，可选值为返回示例中能看到的所有字段。
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 交易编号
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trade.fullinfo.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("tid", this.Tid);
            RequestValidator.ValidateMaxValue("tid", this.Tid, 9223372036854775807);
            RequestValidator.ValidateMinValue("tid", this.Tid, 1);
        }

        #endregion
    }
}
