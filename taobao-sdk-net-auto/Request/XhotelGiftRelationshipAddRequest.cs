using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.gift.relationship.add
    /// </summary>
    public class XhotelGiftRelationshipAddRequest : BaseTopRequest<Top.Api.Response.XhotelGiftRelationshipAddResponse>
    {
        /// <summary>
        /// 礼包ID
        /// </summary>
        public Nullable<long> GiftId { get; set; }

        /// <summary>
        /// relate_id  当TYPE=1时，Dataid填写1，表示设置卖家级别礼包  当TYPE=2时，Dataid填HID  当TYPE=3时，Dataid填GID  当TYPE=4时，Dataid填[GID+RPID]
        /// </summary>
        public string RelateId { get; set; }

        /// <summary>
        /// 礼包关系类型1：Seller，2：Hotel，3：ROOM，4：RatePlan+ROOM
        /// </summary>
        public Nullable<long> Type { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.gift.relationship.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("gift_id", this.GiftId);
            parameters.Add("relate_id", this.RelateId);
            parameters.Add("type", this.Type);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("gift_id", this.GiftId);
            RequestValidator.ValidateRequired("relate_id", this.RelateId);
            RequestValidator.ValidateRequired("type", this.Type);
            RequestValidator.ValidateMaxValue("type", this.Type, 4);
            RequestValidator.ValidateMinValue("type", this.Type, 1);
        }

        #endregion
    }
}
