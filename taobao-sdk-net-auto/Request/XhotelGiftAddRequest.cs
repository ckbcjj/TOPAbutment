using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.gift.add
    /// </summary>
    public class XhotelGiftAddRequest : BaseTopRequest<Top.Api.Response.XhotelGiftAddResponse>
    {
        /// <summary>
        /// 礼包描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public Nullable<DateTime> EndTime { get; set; }

        /// <summary>
        /// 礼包名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public Nullable<DateTime> StartTime { get; set; }

        /// <summary>
        /// status 1：开启（默认）  2：关闭
        /// </summary>
        public Nullable<long> Status { get; set; }

        /// <summary>
        /// 礼包日期类型 1.预订日期2.入住日期
        /// </summary>
        public Nullable<long> TimeType { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.gift.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("desc", this.Desc);
            parameters.Add("end_time", this.EndTime);
            parameters.Add("name", this.Name);
            parameters.Add("start_time", this.StartTime);
            parameters.Add("status", this.Status);
            parameters.Add("time_type", this.TimeType);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("desc", this.Desc);
            RequestValidator.ValidateMaxLength("desc", this.Desc, 300);
            RequestValidator.ValidateRequired("end_time", this.EndTime);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
            RequestValidator.ValidateRequired("start_time", this.StartTime);
            RequestValidator.ValidateRequired("status", this.Status);
            RequestValidator.ValidateRequired("time_type", this.TimeType);
        }

        #endregion
    }
}
