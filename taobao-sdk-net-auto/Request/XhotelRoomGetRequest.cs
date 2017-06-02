using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.room.get
    /// </summary>
    public class XhotelRoomGetRequest : BaseTopRequest<Top.Api.Response.XhotelRoomGetResponse>
    {
        /// <summary>
        /// 废弃
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 外部房型id 如果gid为空，那么out_rid和vendor都不能为空 支持通过gid或者通过out_rid和vendor来获取商品
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 卖家渠道 如果gid为空，那么out_rid和vendor都不能为空。 支持通过gid或者通过out_rid和vendor来获取商品
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.room.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gid", this.Gid);
            parameters.Add("out_rid", this.OutRid);
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
