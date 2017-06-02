using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.refund.refuse
    /// </summary>
    public class RefundRefuseRequest : BaseTopRequest<RefundRefuseResponse>, ITopUploadRequest<RefundRefuseResponse>
    {
        /// <summary>
        /// 退款记录对应的交易子订单号
        /// </summary>
        public Nullable<long> Oid { get; set; }

        /// <summary>
        /// 退款单号
        /// </summary>
        public Nullable<long> RefundId { get; set; }

        /// <summary>
        /// 可选值为：售中：onsale，售后：aftersale，天猫退款为必填项。
        /// </summary>
        public string RefundPhase { get; set; }

        /// <summary>
        /// 退款版本号，天猫退款为必填项。
        /// </summary>
        public Nullable<long> RefundVersion { get; set; }

        /// <summary>
        /// 拒绝退款时的说明信息，长度2-200
        /// </summary>
        public string RefuseMessage { get; set; }

        /// <summary>
        /// 拒绝退款时的退款凭证，一般是卖家拒绝退款时使用的发货凭证，最大长度130000字节，支持的图片格式：GIF, JPG, PNG。天猫退款为必填项。
        /// </summary>
        public FileItem RefuseProof { get; set; }

        /// <summary>
        /// 拒绝原因编号，会提供用户拒绝原因列表供选择
        /// </summary>
        public Nullable<long> RefuseReasonId { get; set; }

        /// <summary>
        /// 退款记录对应的交易订单号
        /// </summary>
        public Nullable<long> Tid { get; set; }

        #region BaseTopRequest Members

        public override string GetApiName()
        {
            return "taobao.refund.refuse";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("oid", this.Oid);
            parameters.Add("refund_id", this.RefundId);
            parameters.Add("refund_phase", this.RefundPhase);
            parameters.Add("refund_version", this.RefundVersion);
            parameters.Add("refuse_message", this.RefuseMessage);
            parameters.Add("refuse_reason_id", this.RefuseReasonId);
            parameters.Add("tid", this.Tid);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("refund_id", this.RefundId);
            RequestValidator.ValidateRequired("refuse_message", this.RefuseMessage);
            RequestValidator.ValidateMaxLength("refuse_message", this.RefuseMessage, 200);
            RequestValidator.ValidateMaxLength("refuse_proof", this.RefuseProof, 130000);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("refuse_proof", this.RefuseProof);
            return parameters;
        }

        #endregion
    }
}
