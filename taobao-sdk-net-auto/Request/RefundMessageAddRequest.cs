using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.refund.message.add
    /// </summary>
    public class RefundMessageAddRequest : BaseTopRequest<RefundMessageAddResponse>, ITopUploadRequest<RefundMessageAddResponse>
    {
        /// <summary>
        /// 留言内容。最大长度: 400个字节
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 图片（凭证）。类型: JPG,GIF,PNG;最大为: 500K
        /// </summary>
        public FileItem Image { get; set; }

        /// <summary>
        /// 退款编号。
        /// </summary>
        public Nullable<long> RefundId { get; set; }

        #region BaseTopRequest Members

        public override string GetApiName()
        {
            return "taobao.refund.message.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("content", this.Content);
            parameters.Add("refund_id", this.RefundId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("content", this.Content);
            RequestValidator.ValidateRequired("image", this.Image);
            RequestValidator.ValidateRequired("refund_id", this.RefundId);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("image", this.Image);
            return parameters;
        }

        #endregion
    }
}
