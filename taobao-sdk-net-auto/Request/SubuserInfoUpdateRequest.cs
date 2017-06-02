using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.subuser.info.update
    /// </summary>
    public class SubuserInfoUpdateRequest : BaseTopRequest<Top.Api.Response.SubuserInfoUpdateResponse>
    {
        /// <summary>
        /// 是否停用子账号 true:表示停用该子账号false:表示开启该子账号
        /// </summary>
        public Nullable<bool> IsDisableSubaccount { get; set; }

        /// <summary>
        /// 子账号是否参与分流 true:参与分流 false:不参与分流
        /// </summary>
        public Nullable<bool> IsDispatch { get; set; }

        /// <summary>
        /// 子账号ID
        /// </summary>
        public Nullable<long> SubId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.subuser.info.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("is_disable_subaccount", this.IsDisableSubaccount);
            parameters.Add("is_dispatch", this.IsDispatch);
            parameters.Add("sub_id", this.SubId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("sub_id", this.SubId);
        }

        #endregion
    }
}
