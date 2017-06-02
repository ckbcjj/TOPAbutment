using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.Entity.OrderEntity
{

    /// <summary>
    /// 订单实体
    /// </summary>
    [XmlRoot("x_hotel_order")]
    public class HotelOrderInfo
    {
        /// <summary>
        /// 酒店订单ID
        /// </summary>
        [XmlElement("oid")]
        public int OrderId { get; set; }
        /// <summary>
        /// 酒店ID
        /// </summary>
        [XmlElement("hid")]
        public int HotelId { set; get; }
        /// <summary>
        /// 房型ID
        /// </summary>
        [XmlElement("rid")]
        public int RoomId { set; get; }
        /// <summary>
        /// 商品ID
        /// </summary>
        [XmlElement("gid")]
        public string Gid { get; set; }
        /// <summary>
        /// RatePlan 中的 rpid
        /// </summary>
        [XmlElement("rpid")]
        public string RPid { get; set; }
        /// <summary>
        /// tid
        /// </summary>
        [XmlElement("tid")]
        public string Tid { get; set; }
        /// <summary>
        /// 房间数
        /// </summary>
        [XmlElement("room_number")]
        public int RoomNum { set; get; }
        /// <summary>
        /// 入住天数
        /// </summary>
        [XmlElement("nights")]
        public int Days { set; get; }
        /// <summary>
        /// 支付类型 可选值 1：预付 5：前台面付
        /// </summary>
        [XmlElement("type")]
        public int Type { get; set; }
        /// <summary>
        /// 住宿起始日期
        /// </summary>
        [XmlElement("checkin_date")]
        public DateTime Sdate { set; get; }
        /// <summary>
        /// 离店日期
        /// </summary>
        [XmlElement("checkout_date")]
        public DateTime Edate { set; get; }
        /// <summary>
        /// 售价  总房价（分）
        /// </summary>
        [XmlElement("total_room_price")]
        public double Price { set; get; }
        /// <summary>
        /// 实付款（分）
        /// </summary>
        [XmlElement("payment")]
        public int Payment { get; set; }
        /// <summary>
        /// 卖家淘宝账号
        /// </summary>
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }
        /// <summary>
        /// 买家淘宝账号
        /// </summary>
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }
        /// <summary>
        /// 订单状态（0为至BOOKING页，2为至支付网关，1为支付成功，-1订单取消，-2订单软删除）
        /// </summary>
        [XmlElement("trade_status")]
        public int Status { set; get; }
        /// <summary>
        /// 入住联系人
        /// </summary>
        [XmlElement("contact_name")]
        public string Contactor { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [XmlElement("contact_phone")]
        public string Mobile { set; get; }
        /// <summary>
        /// 订单生成时间
        /// </summary>
        [XmlElement("created")]
        public DateTime iDate { set; get; }
        /// <summary>
        /// 订单修改时间
        /// </summary>
        [XmlElement("modified")]
        public DateTime Modified { get; set; }
        /// <summary>
        /// 付款时间
        /// </summary>
        [XmlElement("pay_time")]
        public DateTime PayTime { get; set; }
        /// <summary>
        /// 入住人信息
        /// </summary>
        public x_order_guest[] guests { get; set; }
        /// <summary>
        /// 客人订单说明及特殊要求
        /// </summary>
        [XmlElement("message")]
        public string hotelDesc { set; get; }
        /// <summary>
        /// 支付宝交易号，28位字符
        /// </summary>
        [XmlElement("alipay_trade_no")]
        public string AlipayTradeNO { get; set; }
        /// <summary>
        /// 合作方订单号,最长250个字符
        /// </summary>
        [XmlAttribute("out_oid")]
        public string OutOid { get; set; }
        /// <summary>
        /// 最早到店时间
        /// </summary>
        [XmlElement("arrive_early")]
        public DateTime ArriveEarly { set; get; }
        /// <summary>
        /// 最晚到店时间
        /// </summary>
        [XmlElement("arrive_late")]
        public int Lastarrtime { set; get; }
        /// <summary>
        /// 下单时每间夜的价格（分）
        /// </summary>
        [XmlElement("number")]
        public int[] prices { get; set; }


        /// <summary>
        /// 订单渠道来源，与channel关联
        /// </summary>
        public string Webfrom { set; get; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { set; get; }
        /// <summary>
        /// 会员卡号
        /// </summary>
        public int Restcard { set; get; }
        
        /// <summary>
        /// 第三方订单ID
        /// </summary>
        public string PartnerOrderId { set; get; }
        
        /// <summary>
        /// 佣金
        /// </summary>
        public double Commission { set; get; }
        /// <summary>
        /// 应付青芒果金额
        /// </summary>
        public double Ysprice { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public double Ddprice { set; get; }
        /// <summary>
        /// 立减使用值
        /// </summary>
        public double Lijian { set; get; }
        /// <summary>
        /// 红包使用值
        /// </summary>
        public int UrpAmount { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string HotelName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string HotelRoom { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Address { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Tel { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string YuKuan { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string ErrCode { set; get; }
        /// <summary>
        /// 订单处理状态（0未处理），直接与表orderprocstatus关联；1、4、5、6、7、8为确认状态；21为满房，22为变价
        /// </summary>
        public int ProcStatus { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int ProcStatusType { set; get; } //操作状态的类型 1-已确认 2-未确认
        /// <summary>
        /// 资金帐户使用值
        /// </summary>
        public int UbAmount { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string Fax { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string New_SmsUrl { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string cpsIn { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int UN_ComissionProportion { set; get; } //分销订单佣金比例
       
        /// <summary>
        /// 所属频道 web/app/wap
        /// </summary>
        public string channel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string torderid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ResourceID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GuestMobile { get; set; }
    }

    //public class x_order_guest
    //{
    //    /// <summary>
    //    /// 房间序号
    //    /// </summary>
    //    public int room_pos { get; set; }
    //    /// <summary>
    //    /// 入住人序号
    //    /// </summary>
    //    public int person_pos { get; set; }
    //    /// <summary>
    //    /// 入住人姓名
    //    /// </summary>
    //    public string name { get; set; }
    //}
}
