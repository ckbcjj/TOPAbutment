using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.roomtype.get
    /// </summary>
    public class XhotelRoomtypeGetRequest : BaseTopRequest<Top.Api.Response.XhotelRoomtypeGetResponse>
    {
        /// <summary>
        /// 商家房型ID
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// 废弃，使用商家房型ID
        /// </summary>
        public Nullable<long> Rid { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.roomtype.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("rid", this.Rid);
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
