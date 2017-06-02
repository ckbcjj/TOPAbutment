using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.order.update
    /// </summary>
    public class XhotelOrderUpdateRequest : BaseTopRequest<Top.Api.Response.XhotelOrderUpdateResponse>
    {
        /// <summary>
        /// 操作的类型：1.确认无房（取消预订，710发送短信提醒买家申请退款）,2.确认预订
        /// </summary>
        public Nullable<long> OptType { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.order.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("opt_type", this.OptType);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("opt_type", this.OptType);
            RequestValidator.ValidateRequired("tid", this.Tid);
        }

        #endregion
    }
}
