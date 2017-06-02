using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.order.search
    /// </summary>
    public class XhotelOrderSearchRequest : BaseTopRequest<Top.Api.Response.XhotelOrderSearchResponse>
    {
        /// <summary>
        /// 订单创建时间查询结束时间，格式为：yyyy-MM-dd HH:mm:ss。不能早于created_start或者间隔不能大于30
        /// </summary>
        public Nullable<DateTime> CreatedEnd { get; set; }

        /// <summary>
        /// 订单创建时间查询起始时间，格式为：yyyy-MM-dd HH:mm:ss
        /// </summary>
        public Nullable<DateTime> CreatedStart { get; set; }

        /// <summary>
        /// 酒店订单oids列表，多个oid用英文逗号隔开，一次不超过20个。
        /// </summary>
        public string OrderIds { get; set; }

        /// <summary>
        /// 酒店订单tids列表，多个tid用英文逗号隔开，一次不超过20个。oids和tids都传的情况下默认使用tids
        /// </summary>
        public string OrderTids { get; set; }

        /// <summary>
        /// 分页页码。取值范围，大于零的整数，默认值1，即返回第一页的数据。页面大小为20
        /// </summary>
        public Nullable<long> PageNo { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.order.search";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("created_end", this.CreatedEnd);
            parameters.Add("created_start", this.CreatedStart);
            parameters.Add("order_ids", this.OrderIds);
            parameters.Add("order_tids", this.OrderTids);
            parameters.Add("page_no", this.PageNo);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("created_end", this.CreatedEnd);
            RequestValidator.ValidateRequired("created_start", this.CreatedStart);
        }

        #endregion
    }
}
