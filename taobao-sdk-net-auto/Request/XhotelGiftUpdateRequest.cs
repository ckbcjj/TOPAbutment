using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.gift.update
    /// </summary>
    public class XhotelGiftUpdateRequest : BaseTopRequest<Top.Api.Response.XhotelGiftUpdateResponse>
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
        /// 礼包id
        /// </summary>
        public Nullable<long> GiftId { get; set; }

        /// <summary>
        /// 礼包名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public Nullable<DateTime> StartTime { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public Nullable<long> Status { get; set; }

        /// <summary>
        /// 礼包日期类型
        /// </summary>
        public Nullable<long> TimeType { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.gift.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("desc", this.Desc);
            parameters.Add("end_time", this.EndTime);
            parameters.Add("gift_id", this.GiftId);
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
            RequestValidator.ValidateMaxLength("desc", this.Desc, 300);
            RequestValidator.ValidateRequired("gift_id", this.GiftId);
            RequestValidator.ValidateMaxLength("name", this.Name, 60);
        }

        #endregion
    }
}
