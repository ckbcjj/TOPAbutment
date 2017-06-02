using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Domain
{
    /// <summary>
    /// XHotelOrder Data Structure.
    /// </summary>
    [Serializable]
    public class XHotelOrder : TopObject
    {
        /// <summary>
        /// 支付宝交易号，28位字符
        /// </summary>
        [XmlElement("alipay_trade_no")]
        public string AlipayTradeNo { get; set; }

        /// <summary>
        /// 买家最早到达时间
        /// </summary>
        [XmlElement("arrive_early")]
        public string ArriveEarly { get; set; }

        /// <summary>
        /// 买家最晚到达时间
        /// </summary>
        [XmlElement("arrive_late")]
        public string ArriveLate { get; set; }

        /// <summary>
        /// 买家淘宝账号
        /// </summary>
        [XmlElement("buyer_nick")]
        public string BuyerNick { get; set; }

        /// <summary>
        /// 入住时间
        /// </summary>
        [XmlElement("checkin_date")]
        public string CheckinDate { get; set; }

        /// <summary>
        /// 离店时间
        /// </summary>
        [XmlElement("checkout_date")]
        public string CheckoutDate { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        [XmlElement("contact_name")]
        public string ContactName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        [XmlElement("contact_phone")]
        public string ContactPhone { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [XmlElement("end_time")]
        public string EndTime { get; set; }

        /// <summary>
        /// 商品id
        /// </summary>
        [XmlElement("gid")]
        public long Gid { get; set; }

        /// <summary>
        /// 入住人信息
        /// </summary>
        [XmlArray("guests")]
        [XmlArrayItem("x_order_guest")]
        public List<Top.Api.Domain.XOrderGuest> Guests { get; set; }

        /// <summary>
        /// 酒店id
        /// </summary>
        [XmlElement("hid")]
        public long Hid { get; set; }

        /// <summary>
        /// 买家留言
        /// </summary>
        [XmlElement("message")]
        public string Message { get; set; }

        /// <summary>
        /// 订单修改时间
        /// </summary>
        [XmlElement("modified")]
        public string Modified { get; set; }

        /// <summary>
        /// 天数
        /// </summary>
        [XmlElement("nights")]
        public long Nights { get; set; }

        /// <summary>
        /// 酒店订单id
        /// </summary>
        [XmlElement("oid")]
        public long Oid { get; set; }

        /// <summary>
        /// 合作方订单号,最长250个字符
        /// </summary>
        [XmlElement("out_oid")]
        public string OutOid { get; set; }

        /// <summary>
        /// 付款时间
        /// </summary>
        [XmlElement("pay_time")]
        public string PayTime { get; set; }

        /// <summary>
        /// 实付款（分）
        /// </summary>
        [XmlElement("payment")]
        public long Payment { get; set; }

        /// <summary>
        /// 下单时每间夜的价格（分）
        /// </summary>
        [XmlArray("prices")]
        [XmlArrayItem("number")]
        public List<long> Prices { get; set; }

        /// <summary>
        /// 房型id
        /// </summary>
        [XmlElement("rid")]
        public long Rid { get; set; }

        /// <summary>
        /// 房间数
        /// </summary>
        [XmlElement("room_number")]
        public long RoomNumber { get; set; }

        /// <summary>
        /// RatePlan 中的 rpid
        /// </summary>
        [XmlElement("rpid")]
        public long Rpid { get; set; }

        /// <summary>
        /// 卖家淘宝账号
        /// </summary>
        [XmlElement("seller_nick")]
        public string SellerNick { get; set; }

        /// <summary>
        /// tid
        /// </summary>
        [XmlElement("tid")]
        public long Tid { get; set; }

        /// <summary>
        /// 总房价（分）
        /// </summary>
        [XmlElement("total_room_price")]
        public long TotalRoomPrice { get; set; }

        /// <summary>
        /// 交易状态。 WAIT_BUYER_PAY:预订中/等待买家付款, WAIT_SELLER_SEND_GOODS:预订中/等待卖家发货(确认), TRADE_CLOSED:结束/预订失败/交易关闭, TRADE_FINISHED:结束/交易成功, TRADE_NO_CREATE_PAY:结束/预订失败/没有创建支付宝交易, TRADE_CLOSED_BY_TAOBAO:结束/预订失败/预订被卖家关闭, TRADE_SUCCESS:交易中/预订成功/卖家已确认, TRADE_CHECKIN:交易中/预定成功/买家入住, TRADE_CHECKOUT:交易中/预定成功/买家离店, TRADE_SETTLEING:交易中/预定成功/结账中, TRADE_SETTLE_SUCCESS:结束/预定成功/结账成功
        /// </summary>
        [XmlElement("trade_status")]
        public string TradeStatus { get; set; }

        /// <summary>
        /// 支付类型 可选值 1：预付 5：前台面付
        /// </summary>
        [XmlElement("type")]
        public long Type { get; set; }
    }
}
