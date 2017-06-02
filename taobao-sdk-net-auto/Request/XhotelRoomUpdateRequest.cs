using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.room.update
    /// </summary>
    public class XhotelRoomUpdateRequest : BaseTopRequest<XhotelRoomUpdateResponse>, ITopUploadRequest<XhotelRoomUpdateResponse>
    {
        /// <summary>
        /// 保留房库存截止时间
        /// </summary>
        public string AllotmentEndTime { get; set; }

        /// <summary>
        /// 保留房库存截止时间
        /// </summary>
        public string AllotmentStartTime { get; set; }

        /// <summary>
        /// 废弃，宝贝描述展示在宝贝详情页面
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 废弃，使用out_rid
        /// </summary>
        public Nullable<long> Gid { get; set; }

        /// <summary>
        /// 废弃，房型购买须知展示在PC购物路径
        /// </summary>
        public string Guide { get; set; }

        /// <summary>
        /// 废弃，房型是否提供发票
        /// </summary>
        public Nullable<bool> HasReceipt { get; set; }

        /// <summary>
        /// 房型共享库存日历。quota物理库存；al_quota保留房库存；sp_quota超预定库存。其中保留房库存和超预定库存需要向运营申请权限示例：[{"date":2011-01-28,"quota":10,"al_quota":2,"sp_quota":3}]
        /// </summary>
        public string Inventory { get; set; }

        /// <summary>
        /// 卖家房型ID
        /// </summary>
        public string OutRid { get; set; }

        /// <summary>
        /// 废弃，宝贝图片，没有默认使用标准酒店房型图片
        /// </summary>
        public FileItem Pic { get; set; }

        /// <summary>
        /// 废弃，房型发票说明，不能超过100个字
        /// </summary>
        public string ReceiptInfo { get; set; }

        /// <summary>
        /// 废弃，房型发票类型为其他时的发票描述,不能超过30个字
        /// </summary>
        public string ReceiptOtherTypeDesc { get; set; }

        /// <summary>
        /// 废弃，房型发票类型。A,B。分别代表： A:酒店住宿发票,B:其他
        /// </summary>
        public string ReceiptType { get; set; }

        /// <summary>
        /// 房型库存开关。 1，开；2，关
        /// </summary>
        public string RoomSwitchCal { get; set; }

        /// <summary>
        /// 超预定库存截止时间
        /// </summary>
        public string SuperbookEndTime { get; set; }

        /// <summary>
        /// 超预定库存开始时间
        /// </summary>
        public string SuperbookStartTime { get; set; }

        /// <summary>
        /// 废弃，宝贝名称展示在店铺里
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 系统商，一般不填写，使用须申请
        /// </summary>
        public string Vendor { get; set; }

        #region BaseTopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.room.update";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("allotment_end_time", this.AllotmentEndTime);
            parameters.Add("allotment_start_time", this.AllotmentStartTime);
            parameters.Add("desc", this.Desc);
            parameters.Add("gid", this.Gid);
            parameters.Add("guide", this.Guide);
            parameters.Add("has_receipt", this.HasReceipt);
            parameters.Add("inventory", this.Inventory);
            parameters.Add("out_rid", this.OutRid);
            parameters.Add("receipt_info", this.ReceiptInfo);
            parameters.Add("receipt_other_type_desc", this.ReceiptOtherTypeDesc);
            parameters.Add("receipt_type", this.ReceiptType);
            parameters.Add("room_switch_cal", this.RoomSwitchCal);
            parameters.Add("superbook_end_time", this.SuperbookEndTime);
            parameters.Add("superbook_start_time", this.SuperbookStartTime);
            parameters.Add("title", this.Title);
            parameters.Add("vendor", this.Vendor);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("desc", this.Desc, 50000);
            RequestValidator.ValidateMaxLength("guide", this.Guide, 600);
            RequestValidator.ValidateMaxLength("out_rid", this.OutRid, 50);
            RequestValidator.ValidateMaxLength("pic", this.Pic, 512000);
            RequestValidator.ValidateMaxLength("receipt_info", this.ReceiptInfo, 100);
            RequestValidator.ValidateMaxLength("receipt_other_type_desc", this.ReceiptOtherTypeDesc, 30);
            RequestValidator.ValidateMaxLength("title", this.Title, 60);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("pic", this.Pic);
            return parameters;
        }

        #endregion
    }
}
