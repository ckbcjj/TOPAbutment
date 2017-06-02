using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.rate.get
    /// </summary>
    public class XhotelRateGetRequest : BaseTopRequest<Top.Api.Response.XhotelRateGetResponse>
    {
        /// <summary>
        /// gid酒店商品id
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 卖家房型ID, 这是卖家自己系统中的房型ID 注意：需要按照规则组合
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 卖家自己系统的Code，简称RateCode
        /// </summary>
        public string RateplanCode { get; set; }

        /// <summary>
        /// 酒店RPID
        /// </summary>
        public Nullable<long> Rpid { get; set; }

        /// <summary>
        /// 用于标示该宝贝的售卖渠道信息，允许同一个卖家酒店房型在淘宝系统发布多个售卖渠道的宝贝的价格。
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.rate.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gid", this.Gid);
            parameters.Add("out_rid", this.OutRid);
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
            RequestValidator.ValidateMaxLength("out_rid", this.OutRid, 128);
            RequestValidator.ValidateMaxLength("rateplan_code", this.RateplanCode, 128);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
