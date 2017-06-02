using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.subuser.fullinfo.get
    /// </summary>
    public class SubuserFullinfoGetRequest : BaseTopRequest<Top.Api.Response.SubuserFullinfoGetResponse>
    {
        /// <summary>
        /// 传入所需要的参数信息（若不需要获取子账号或主账号的企业邮箱地址，则无需传入该参数；若需要获取子账号或主账号的企业邮箱地址，则需要传入fields；可选参数值为subuser_email和user_email，传入其他参数值均无效；两个参数都需要则以逗号隔开传入即可，例如：subuser_email,user_email）
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 子账号ID（传参中sub_id和sub_nick至少需要其中一个，若sub_id与sub_nick同时传入并且合法，那么sub_nick优先，以sub_nick查询子账号）
        /// </summary>
        public Nullable<long> SubId { get; set; }

        /// <summary>
        /// 子账号用户名（传参中sub_id和sub_nick至少需要其中一个，若sub_id与sub_nick同时传入并且合法，那么sub_nick优先，以sub_nick查询子账号）
        /// </summary>
        public string SubNick { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.subuser.fullinfo.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
            parameters.Add("sub_id", this.SubId);
            parameters.Add("sub_nick", this.SubNick);
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
