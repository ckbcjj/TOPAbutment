using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.trades.sold.increment.get
    /// </summary>
    public class TradesSoldIncrementGetRequest : BaseTopRequest<Top.Api.Response.TradesSoldIncrementGetResponse>
    {
        /// <summary>
        /// 买家昵称
        /// </summary>
        public string BuyerNick { get; set; }

        /// <summary>
        /// 查询修改结束时间，必须大于修改开始时间(修改时间跨度不能大于一天)，格式:yyyy-MM-dd HH:mm:ss。<span style="color:red;font-weight: bold;">建议使用30分钟以内的时间跨度，能大大提高响应速度和成功率</span>。
        /// </summary>
        public Nullable<DateTime> EndModified { get; set; }

        /// <summary>
        /// 可选值有ershou(二手市场的订单）,service（商城服务子订单）mark（双十一大促活动异常订单）作为扩展类型筛选只能做单个ext_type查询，不能全部查询所有的ext_type类型
        /// </summary>
        public string ExtType { get; set; }

        /// <summary>
        /// 需要返回的字段列表，多个字段用半角逗号分隔，可选值为返回示例中能看到的所有字段。rx_audit_status=0为处方药未审核状态
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 页码。取值范围:大于零的整数;默认值:1。<span style="color:red;font-weight: bold;">注：必须采用倒序的分页方式（从最后一页往回取）才能避免漏单问题。</span>
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        /// <summary>
        /// 每页条数。取值范围：1~100，默认值：40。<span style="color:red;font-weight: bold;">建议使用40~50，可以提高成功率，减少超时数量</span>。
        /// </summary>
        public Nullable<long> PageSize { get; set; }

        /// <summary>
        /// 评价状态，默认查询所有评价状态的数据，除了默认值外每次只能查询一种状态。<br>可选值：RATE_UNBUYER(买家未评)RATE_UNSELLER(卖家未评)RATE_BUYER_UNSELLER(买家已评，卖家未评)RATE_UNBUYER_SELLER(买家未评，卖家已评)RATE_BUYER_SELLER(买家已评,卖家已评)
        /// </summary>
        public string RateStatus { get; set; }

        /// <summary>
        /// 查询修改开始时间(修改时间跨度不能大于一天)。格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        public Nullable<DateTime> StartModified { get; set; }

        /// <summary>
        /// 交易状态（<a href="http://open.taobao.com/doc/detail.htm?id=102856" target="_blank">查看可选值</a>），默认查询所有交易状态的数据，除了默认值外每次只能查询一种状态。
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 卖家对交易的自定义分组标签，目前可选值为：time_card（点卡软件代充），fee_card（话费软件代充）
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 交易类型列表（<a href="http://open.taobao.com/doc/detail.htm?id=102855" target="_blank">查看可选值</a>），一次查询多种类型可用半角逗号分隔，默认同时查询guarantee_trade,auto_delivery,ec,cod,step这5种类型的数据。
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 是否启用has_next的分页方式，如果指定true,则返回的结果中不包含总记录数，但是会新增一个是否存在下一页的的字段，<span style="color:red;font-weight: bold;">通过此种方式获取增量交易，效率在原有的基础上有80%的提升</span>。
        /// </summary>
        public Nullable<bool> UseHasNext { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.trades.sold.increment.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("buyer_nick", this.BuyerNick);
            parameters.Add("end_modified", this.EndModified);
            parameters.Add("ext_type", this.ExtType);
            parameters.Add("fields", this.Fields);
            parameters.Add("page_no", this.PageNo);
            parameters.Add("page_size", this.PageSize);
            parameters.Add("rate_status", this.RateStatus);
            parameters.Add("start_modified", this.StartModified);
            parameters.Add("status", this.Status);
            parameters.Add("tag", this.Tag);
            parameters.Add("type", this.Type);
            parameters.Add("use_has_next", this.UseHasNext);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("end_modified", this.EndModified);
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("start_modified", this.StartModified);
        }

        #endregion
    }
}
