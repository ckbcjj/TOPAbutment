using System;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.xhotel.roomtype.add
    /// </summary>
    public class XhotelRoomtypeAddRequest : BaseTopRequest<Top.Api.Response.XhotelRoomtypeAddResponse>
    {
        /// <summary>
        /// 具体面积大小，请按照正确格式填写。两种格式，例如：40或者 10-20
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 床宽。按自己定义存储，比如：2.1米
        /// </summary>
        public string BedSize { get; set; }

        /// <summary>
        /// 床型。按链接中床型列表定义值存储 http://open.taobao.com/docs/doc.htm?&docType=1&articleId=105610
        /// </summary>
        public string BedType { get; set; }

        /// <summary>
        /// 不要使用
        /// </summary>
        public string Extend { get; set; }

        /// <summary>
        /// 客房在建筑的第几层，隔层为1-2层，4-5层，7-8层
        /// </summary>
        public string Floor { get; set; }

        /// <summary>
        /// （已废弃）请使用outHid
        /// </summary>
        public Nullable<long> Hid { get; set; }

        /// <summary>
        /// 宽带服务。A,B,C,D。分别代表： A：无宽带，B：免费宽带，C：收费宽带，D：部分收费宽带
        /// </summary>
        public string Internet { get; set; }

        /// <summary>
        /// 最大入住人数，默认2（1-99）
        /// </summary>
        public Nullable<long> MaxOccupancy { get; set; }

        /// <summary>
        /// 房型名称。不能超过30字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// （必传）商家酒店ID，指明该房型属于哪家酒店
        /// </summary>
        public string OutHid { get; set; }

        /// <summary>
        /// 卖家房型ID，不能重复建议格式是:酒店code_房型code
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// 房型图片只支持远程图片，格式如下：[{"url":"http://taobao.com/123.jpg","ismain":"true"},{"url":"http://taobao.com/456.jpg","ismain":"false"},{"url":"http://taobao.com/789.jpg","ismain":"false"}]其中url是远程图片的访问地址（URL地址必须是合法的，否则会报错），main是是否为主图。只能设置一张图片为主图。
        /// </summary>
        public string Pics { get; set; }

        /// <summary>
        /// 设施服务。JSON格式。 value值true有此服务，false没有。 bar：吧台，catv：有线电视，ddd：国内长途电话，idd：国际长途电话，toilet：独立卫生间，pubtoliet：公共卫生间。 如： {"bar":false,"catv":false,"ddd":false,"idd":false,"pubtoilet":false,"toilet":false}
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 该字段只有确定的时候，才允许填入。用于标示和淘宝房型的匹配关系。目前尚未启动该字段。
        /// </summary>
        public Nullable<long> Srid { get; set; }

        /// <summary>
        /// 系统商，无申请不可使用
        /// </summary>
        public string Vendor { get; set; }

        /// <summary>
        /// 0:无窗/1:有窗
        /// </summary>
        public Nullable<long> WindowType { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.xhotel.roomtype.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("area", this.Area);
            parameters.Add("bed_size", this.BedSize);
            parameters.Add("bed_type", this.BedType);
            parameters.Add("extend", this.Extend);
            parameters.Add("floor", this.Floor);
            parameters.Add("hid", this.Hid);
            parameters.Add("internet", this.Internet);
            parameters.Add("max_occupancy", this.MaxOccupancy);
            parameters.Add("name", this.Name);
            parameters.Add("out_hid", this.OutHid);
            parameters.Add("outer_id", this.OuterId);
            parameters.Add("pics", this.Pics);
            parameters.Add("service", this.Service);
            parameters.Add("srid", this.Srid);
            parameters.Add("vendor", this.Vendor);
            parameters.Add("window_type", this.WindowType);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxLength("area", this.Area, 30);
            RequestValidator.ValidateMaxLength("bed_size", this.BedSize, 30);
            RequestValidator.ValidateRequired("bed_type", this.BedType);
            RequestValidator.ValidateMaxLength("bed_type", this.BedType, 30);
            RequestValidator.ValidateMaxLength("extend", this.Extend, 500);
            RequestValidator.ValidateMaxLength("floor", this.Floor, 30);
            RequestValidator.ValidateRequired("name", this.Name);
            RequestValidator.ValidateMaxLength("name", this.Name, 30);
            RequestValidator.ValidateMaxLength("out_hid", this.OutHid, 50);
            RequestValidator.ValidateRequired("outer_id", this.OuterId);
            RequestValidator.ValidateMaxLength("outer_id", this.OuterId, 64);
            RequestValidator.ValidateMaxLength("service", this.Service, 1024);
            RequestValidator.ValidateMaxLength("vendor", this.Vendor, 50);
        }

        #endregion
    }
}
