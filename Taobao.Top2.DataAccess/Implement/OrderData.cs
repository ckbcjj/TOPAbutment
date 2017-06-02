using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taobao.Top2.Entity.OrderEntity;
using System.Data.SqlClient;
using System.Data;
using Common.Tool;
using System.Net;
using System.IO;

namespace Taobao.Top2.DataAccess.Implement
{
    class OrderData : IOrderData
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// 获取上传淘宝酒店信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns></returns>
        public DataTable GetHotelById(List<string> hid)
        {
            string str = string.Join("','", hid);
            string format = @"SELECT * from taobao_new_hotel WHERE hid IN ('{0}')";
            return SqlHelper.ExcuteDataTable(string.Format(format, str), null, SqlHelper.conStrRead);
        }


        /// <summary>
        /// 获取在售酒店信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns></returns>
        public DataTable GetSaleHotelById(List<string> hotelid)
        {
            string str = string.Join("','", hotelid);
            string format = @"select distinct hotelid from hotels where status=1 and hotelid in ('{0}')";
            return SqlHelper.ExcuteDataTable(string.Format(format, str), null, SqlHelper.conStrRead);
        }


        /// <summary>
        /// 获取上传淘宝房型信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns></returns>
        public DataTable GetRoomsById(List<string> rpid)
        {
            string str = string.Join("','", rpid);
            string format = @"SELECT distinct qmg_hotelid,qmg_roomid,rid,hid from Taobao_new_Product_his  WHERE rpid IN ('{0}')";
            return SqlHelper.ExcuteDataTable(string.Format(format, str), null, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 获取在售房型信息
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public DataTable GetSaleRoomById(List<string> hotelid, List<string> roomid)
        {
            string str1 = string.Join("','", hotelid);
            string str2 = string.Join("','", roomid);
            string format = @"select distinct room from hotelroom where status=1 and hotelid in  ('{0}') and room in  ('{1}')";
            return SqlHelper.ExcuteDataTable(string.Format(format, str1, str2), null, SqlHelper.conStrRead);
        }



        /// <summary>
        /// 判断酒店类型
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns>110:发现假期  0:自签  114:99连锁酒店  115:云掌柜</returns>
        private int GetHotelType(int hotelid)
        {
            SqlParameter[] sps = new SqlParameter[1];
            sps[0] = new SqlParameter("@hotelId", SqlDbType.Int);
            sps[0].Value = hotelid;
            string sqls = @"SELECT resourceid
                           FROM hotels 
                           WHERE hotelid=@hotelId";
            DataTable dt = SqlHelper.ExcuteDataTable(sqls, sps, SqlHelper.conStrRead);
            if (dt != null && dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0].ItemArray[0].ToString());
            }
            return 0;
        }

        /// <summary>
        /// 判断是否为香港
        /// </summary>
        /// <param name="hotelid"></param>
        /// <returns>true:香港</returns>
        private bool GetHongKongType(int hotelid)
        {
            //判断城市是否为香港
            SqlParameter[] sp = new SqlParameter[1];
            sp[0] = new SqlParameter("@hotelId", SqlDbType.Int);
            sp[0].Value = hotelid;
            string sql = @"SELECT province
                           FROM hotels 
                           WHERE hotelid=@hotelId";
            DataTable dt = SqlHelper.ExcuteDataTable(sql, sp, SqlHelper.conStrRead);

            //自签香港酒店
            if (dt != null && dt.Rows.Count > 0)
            {
                //香港
                if (dt.Rows[0].ItemArray[0].ToString() == "32")
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 生成新订单
        /// </summary>
        /// <param name="hotelOrderInfo">订单实体</param>
        /// <returns>返回订单号</returns>
        public int AddNewOrder(HotelOrderInfo hotelOrderInfo)
        {
            SqlParameter[] sp = new SqlParameter[5];
            sp[0] = new SqlParameter("@hotelid", SqlDbType.Int);
            sp[0].Value = hotelOrderInfo.HotelId;

            sp[1] = new SqlParameter("@sdate", SqlDbType.DateTime);
            sp[1].Value = hotelOrderInfo.Sdate;

            sp[2] = new SqlParameter("@edate", SqlDbType.DateTime);
            sp[2].Value = hotelOrderInfo.Edate;

            sp[3] = new SqlParameter("@days", SqlDbType.Int);
            sp[3].Value = hotelOrderInfo.Days;

            sp[4] = new SqlParameter("@webfrom", SqlDbType.VarChar);
            sp[4].Value = hotelOrderInfo.Webfrom;

            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable("sp1_hostel_newOrder", CommandType.StoredProcedure, sp);
                hotelOrderInfo.OrderId = Convert.ToInt32(dt.Rows[0]["orderid"].ToString());
                return hotelOrderInfo.OrderId;
            }
            catch (Exception e)
            {
                log.Error("生成新订单异常", e);
                return 0;
            }
        }

        /// <summary>
        /// 生成房型数据并更新订单数据
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        /// <returns></returns>
        public HotelOrderInfo AddOrderRoom(HotelOrderInfo hotelOrderInfo)
        {
            hotelOrderInfo.ProcStatus = 0;

            int dayCount = 0;
            //变量定义
            double priceCount = 0;
            //价格
            double rprice = 0;
            //成本
            double rcost = 0;
            //佣金
            double commission = 0;
            double totalcom = 0;
            double persons = 0;
            double surpOrderAmount = 0;
            double rysprice = 0;
            int persons2 = 0;
            double price = 0;
            double sFee = 0;
            double ipayAmount = 0;
            double urpOrderAmount = 0;
            double commentFreeFee = 0;
            //应付青芒果金额
            double ysPrice = 0;
            int days = hotelOrderInfo.Days;
            double islijian = 0;
            //double bonus = 0;

            //是否立减
            bool ifcanlijian = false;
            //立减比例
            double lijianProportion = 0;
            //增幅比例
            double zengfuPropertion = 0;
            //数据库取出的值用变量定义保存
            double eprice_field = 0;
            int persons_field = 0;
            double cost_field = 0;
            double com_field = 0;
            //红包使用额度
            double redpacketuseLimit_field = 0;
            int persons_roomnums = 0;//间数
            double balance = 0;
            string balanceType = "";
            int leftBeds = 0;
            bool IsAgent = false;

            int hotelType = GetHotelType(hotelOrderInfo.HotelId);

            for (int k = 1; k <= days; k++)
            {
                persons = 0;
                price = 0;
                //入住酒店信息
                string strsql = @"select hp.*,isnull(hp.commission,0) com ,hm.hotelid,hm.roomname,hm.isshared,hm.hasdlwy,hm.persons,
                                        hm.roomnums,hm.breakfasts,hm.bednums,hm.redpacketuseLimit,h.commentReturnlimit,hm.ttslijian,
                                        hm.ctriplijian 
                                  from hotelroom hm 
                                  left join hotelprice hp 
                                  on hm.room=hp.room 
                                  left Join hotels h 
                                  On h.hotelid = hm.hotelid 
                                  where(hm.istuan=0 or hm.istuan is null) and hm.HotelID=" + hotelOrderInfo.HotelId + " and hp.effectdate='" + hotelOrderInfo.Sdate + "'";
                DataTable dt = SqlHelper.ExecuteDataTable(strsql);
                foreach (DataRow dr in dt.Rows)
                {
                    if (Convert.ToInt32(hotelOrderInfo.RoomId.ToString()) == Convert.ToInt32(dr["room"].ToString()))
                    {
                        dayCount++;
                        DataTable dtable = GetBatchRoomList(hotelOrderInfo.HotelId, Convert.ToInt32(dr["room"].ToString()));
                        if (dtable != null && dtable.Rows.Count != 0)
                        {
                            lijianProportion = Convert.ToInt32(dtable.Rows[0]["lijian"].ToString());
                            zengfuPropertion = Convert.ToInt32(dtable.Rows[0]["zengfu"].ToString());
                        }
                        if (lijianProportion > 0)
                        {
                            ifcanlijian = true;
                        }
                        if (zengfuPropertion > 0)
                        {
                            eprice_field = System.Math.Ceiling((Convert.ToDouble(dr["eprice"].ToString()) - Convert.ToDouble(dr["com"].ToString())) * (1 + zengfuPropertion / 100));
                        }
                        else
                        {
                            eprice_field = Convert.ToDouble(dr["eprice"].ToString());
                        }
                        persons_field = Convert.ToInt32(dr["persons"].ToString());
                        cost_field = Convert.ToDouble(dr["cost"].ToString());
                        com_field = Math.Floor(eprice_field - (Convert.ToDouble(dr["eprice"].ToString()) - Convert.ToDouble(dr["com"].ToString())));
                        redpacketuseLimit_field = Convert.ToInt32(dr["redpacketuseLimit"].ToString());
                        persons_roomnums = Convert.ToInt32(hotelOrderInfo.RoomNum.ToString());
                        leftBeds = Convert.ToInt32(dr["eBeds"].ToString()) - Convert.ToInt32(dr["avieBeds"].ToString());
                        //总可售数-已用可售数    剩余= eBeds- avieBeds
                        if (Convert.ToInt32(dr["ebeds"].ToString()) - Convert.ToInt32(dr["avieBeds"].ToString()) <= 0 || eprice_field == 0)
                        {
                            //订单处理状态 满房
                            hotelOrderInfo.ProcStatus = 21;
                        }

                        //床位房，价格是单床价格  佣金是全部佣金
                        if (dr["isshared"].ToString().Trim() == "T")
                        {
                            rprice = eprice_field / persons_field;   // 价格/每个房间人数=每个床位价格
                            rcost = cost_field / persons_field;      // 成本/每个房间人数=每个床位成本
                            commission = com_field / persons_field * persons_roomnums; // 房间数*佣金/每个房间人数
                            totalcom = totalcom + commission;        // 总佣金
                            persons = persons + persons_roomnums;    // 总人数
                            surpOrderAmount = GetRound(com_field / persons_field * 1 * redpacketuseLimit_field * 0.01 * persons_roomnums, 0);
                            persons2 = persons_roomnums;
                            //bonus = Convert.ToInt32(GetRound(Convert.ToDouble(com_field / persons_field * persons_roomnums * 1 * Convert.ToInt32(dr["commentReturnlimit"].ToString()) / 100), 0));
                        }
                        //普通房，价格和佣金都是全部
                        else
                        {
                            rprice = eprice_field * persons_roomnums;
                            rcost = cost_field * persons_roomnums;
                            commission = com_field * persons_roomnums;
                            totalcom = totalcom + commission;
                            persons = persons + persons_roomnums * persons_field;
                            surpOrderAmount = GetRound(com_field * 1 * redpacketuseLimit_field * 0.01 * persons_roomnums, 0);
                            persons2 = persons_roomnums * persons_field;
                            //bonus = Convert.ToInt32(GetRound(Convert.ToDouble(com_field * persons_roomnums * 1 * Convert.ToInt32(dr["commentReturnlimit"].ToString()) / 100), 0));
                        }

                        double xlijian = 0;
                        //汇总立减数据
                        if (ifcanlijian)
                        {
                            xlijian = System.Math.Floor(commission - commission * (100 - lijianProportion) / 100);
                            islijian += xlijian;
                        }


                        rysprice = rprice;
                        if (ifcanlijian)
                        {
                            rysprice = System.Math.Ceiling(rprice - xlijian);
                        }

                        SqlParameter[] sp = new SqlParameter[12];
                        sp[0] = new SqlParameter("@orderid", SqlDbType.Int);
                        sp[0].Value = hotelOrderInfo.OrderId;

                        sp[1] = new SqlParameter("@hotelid", SqlDbType.Int);
                        sp[1].Value = hotelOrderInfo.HotelId;

                        sp[2] = new SqlParameter("@room", SqlDbType.Int);
                        sp[2].Value = dr["room"].ToString();

                        sp[3] = new SqlParameter("@sdate", SqlDbType.DateTime);
                        sp[3].Value = hotelOrderInfo.Sdate;

                        //入住人数
                        sp[4] = new SqlParameter("@persons", SqlDbType.Int);
                        sp[4].Value = persons2;

                        //售价
                        sp[5] = new SqlParameter("@price", SqlDbType.Float);
                        sp[5].Value = int.Parse(GetRound(rprice, 0).ToString());

                        //成本
                        sp[6] = new SqlParameter("@cost", SqlDbType.Float);
                        sp[6].Value = rcost;

                        //佣金
                        sp[7] = new SqlParameter("@commission", SqlDbType.Float);
                        sp[7].Value = int.Parse(GetRound(commission, 0).ToString());

                        sp[8] = new SqlParameter("@isshared", SqlDbType.VarChar);

                        sp[8].Value = dr["isshared"].ToString();

                        sp[9] = new SqlParameter("@ysprice", SqlDbType.Float);
                        sp[9].Value = rysprice;

                        //红包
                        sp[10] = new SqlParameter("@redPacket", SqlDbType.Float);
                        sp[10].Value = surpOrderAmount;

                        //点评返现
                        sp[11] = new SqlParameter("@bonus", SqlDbType.Float);
                        sp[11].Value = 0;

                        try
                        {
                            //只增加房型信息存储过程
                            SqlHelper.ExecuteNonQuery("sp3_HotelOrderRooms_i", CommandType.StoredProcedure, sp);
                        }
                        catch (Exception e)
                        {
                            log.Error("增加订单详细房型信息异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                            return hotelOrderInfo;
                        }


                        if (dr["isshared"].ToString().Trim() == "T")
                        {
                            price = price + eprice_field / persons_field * persons_roomnums;
                            sFee = sFee + eprice_field / persons_field * persons_roomnums;
                            ipayAmount = ipayAmount + (eprice_field - cost_field) / persons_field * persons_roomnums;
                            commentFreeFee = commentFreeFee + com_field / persons_field * persons_roomnums * 1 * Convert.ToInt32(dr["commentReturnlimit"].ToString()) / 100;
                            //红包
                            //urpOrderAmount = urpOrderAmount + Convert.ToInt32(GetRound(Convert.ToDouble(com_field / persons_field * persons_roomnums * 1 * redpacketuseLimit_field * 0.01), 0));
                        }
                        else
                        {
                            price = price + eprice_field * persons_roomnums;
                            sFee = sFee + eprice_field * persons_roomnums;
                            ipayAmount = ipayAmount + (eprice_field - cost_field) * persons_roomnums;
                            commentFreeFee = commentFreeFee + com_field * persons_roomnums * 1 * Convert.ToInt32(dr["commentReturnlimit"].ToString()) / 100;
                            //红包
                            //urpOrderAmount = urpOrderAmount + Convert.ToInt32(GetRound(Convert.ToDouble(com_field * persons_roomnums * 1 * redpacketuseLimit_field * 0.01), 0));
                        }
                    }
                }
                hotelOrderInfo.Sdate = hotelOrderInfo.Sdate.AddDays(1);
            }


            if ((ipayAmount - islijian) > 0)
            {
                ipayAmount = ipayAmount - islijian;
            }
            else
            {
                islijian = 0;
            }


            urpOrderAmount = 0;
            ysPrice = ipayAmount;

            string restcard = hotelOrderInfo.Restcard.ToString();

            //SendTTSDataInfo sendtts = new SendTTSDataInfo();
            if (ifcanlijian && -2 * hotelOrderInfo.Days < hotelOrderInfo.Price - (hotelOrderInfo.Payment / 100 + islijian) && 2 * hotelOrderInfo.Days > hotelOrderInfo.Price - (hotelOrderInfo.Payment / 100 + islijian))
            {
                islijian = sFee - hotelOrderInfo.Payment / 100;
            }



            SqlParameter[] sp1 = new SqlParameter[15];
            sp1[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp1[0].Value = hotelOrderInfo.OrderId;

            sp1[1] = new SqlParameter("@status", SqlDbType.Int);
            hotelOrderInfo.Status = 2;
            sp1[1].Value = hotelOrderInfo.Status;

            sp1[2] = new SqlParameter("@contactor", SqlDbType.VarChar);
            sp1[2].Value = hotelOrderInfo.Contactor;

            sp1[3] = new SqlParameter("@gender", SqlDbType.VarChar);
            sp1[3].Value = hotelOrderInfo.Gender;

            sp1[4] = new SqlParameter("@city", SqlDbType.Int);
            sp1[4].Value = 0;

            sp1[5] = new SqlParameter("@mobile", SqlDbType.VarChar);
            sp1[5].Value = hotelOrderInfo.Mobile;

            sp1[6] = new SqlParameter("@webfrom", SqlDbType.VarChar);
            sp1[6].Value = hotelOrderInfo.Webfrom;

            sp1[7] = new SqlParameter("@payfs", SqlDbType.VarChar);
            sp1[7].Value = "ubk";

            sp1[8] = new SqlParameter("@ubAmount", SqlDbType.Int);
            sp1[8].Value = int.Parse(GetRound(ipayAmount, 0).ToString());

            sp1[9] = new SqlParameter("@urpAmount", SqlDbType.Int);
            sp1[9].Value = urpOrderAmount;

            sp1[10] = new SqlParameter("@lastarrtime", SqlDbType.Int);
            sp1[10].Value = hotelOrderInfo.Lastarrtime;

            sp1[11] = new SqlParameter("@islijian", SqlDbType.Int);
            sp1[11].Value = int.Parse(GetRound(islijian, 0).ToString());

            sp1[12] = new SqlParameter("@partnerOrderId", SqlDbType.VarChar);
            sp1[12].Value = 0;

            sp1[13] = new SqlParameter("@restcard", SqlDbType.VarChar);
            sp1[13].Value = restcard;

            sp1[14] = new SqlParameter("@hotelDesc", SqlDbType.VarChar);
            sp1[14].Value = hotelOrderInfo.hotelDesc;

            try
            {
                //调用sp3_hotelorders_u
                SqlHelper.ExecuteNonQuery("sp1_hotelorders_upd", CommandType.StoredProcedure, sp1);
            }
            catch (Exception e)
            {
                log.Error("更新订单数据sp1_hotelorders_upd异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                return hotelOrderInfo;
            }

            //淘宝来源的订单，单号需要写入到torderid字段中
            SqlParameter[] sp1_1 = new SqlParameter[15];
            sp1_1[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp1_1[0].Value = hotelOrderInfo.OrderId;
            sp1_1[1] = new SqlParameter("@torderid", SqlDbType.VarChar);
            sp1_1[1].Value = hotelOrderInfo.PartnerOrderId;
            try
            {
                SqlHelper.ExecuteNonQuery("sp3_hotelorders_u", CommandType.StoredProcedure, sp1_1);
            }
            catch (Exception e)
            {
                log.Error("更新订单数据sp3_hotelorders_u异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                return hotelOrderInfo;
            }

            //如果是满房状态，不落单
            if (hotelOrderInfo.ProcStatus == 21)
            {
                return hotelOrderInfo;
            }


            if (persons == 0 || sFee == 0 || ysPrice == 0)
            {
                log.Error("数据异常 persons:" + persons + " sFree:" + sFee + " ysPrice:" + ysPrice);
                return hotelOrderInfo;
            }

            hotelOrderInfo.Price = sFee;
            hotelOrderInfo.Ysprice = ysPrice;
            hotelOrderInfo.Commission = totalcom;

            SqlParameter[] sp2 = new SqlParameter[5];
            sp2[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp2[0].Value = hotelOrderInfo.OrderId;

            sp2[1] = new SqlParameter("@persons", SqlDbType.Int);
            sp2[1].Value = persons;

            sp2[2] = new SqlParameter("@price", SqlDbType.Int);
            sp2[2].Value = GetRound(sFee, 0);

            sp2[3] = new SqlParameter("@commission", SqlDbType.Float);
            sp2[3].Value = GetRound(hotelOrderInfo.Commission * 100, 0) / 100;//保留两位小数

            sp2[4] = new SqlParameter("@ysPrice", SqlDbType.Int);
            sp2[4].Value = GetRound(ysPrice, 0);

            try
            {
                SqlHelper.ExecuteNonQuery("sp1_hostel_UpdOrder", CommandType.StoredProcedure, sp2);
            }
            catch (Exception e)
            {
                log.Error("更新订单数据sp1_hostel_UpdOrder异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);

                return hotelOrderInfo;
            }


            UserDAL userDal = new UserDAL();
            int ubank = userDal.GetUserBank(restcard);
            if (ubank < ipayAmount)
            {
                log.Error("资金账户余额不足OrderId:" + hotelOrderInfo.OrderId);
                return hotelOrderInfo;
            }

            //扣款
            if (!userDal.UserKK(restcard, GetRound(ysPrice, 0).ToString(), urpOrderAmount.ToString(), hotelOrderInfo.OrderId.ToString()))
            {
                log.Error("扣款失败  参数:" + restcard + " " + GetRound(ysPrice, 0).ToString() + " " + urpOrderAmount.ToString() + " " + hotelOrderInfo.OrderId.ToString());
                return hotelOrderInfo;
            }

            //会员注册
            Register(hotelOrderInfo);
            AddFinanceDate(hotelOrderInfo);


            if (hotelType == 0 && hotelOrderInfo.ProcStatus != 21)
            {
                GetUrl("http://fax.qmango.com/qtsms/testsms.asp?orderid=" + hotelOrderInfo.OrderId);
            }


            //更新订单状态（按分计算）
            //SendTTSDataInfo sendtts = new SendTTSDataInfo();
            if (-2 * hotelOrderInfo.Days < hotelOrderInfo.Price - (hotelOrderInfo.Payment / 100 + islijian) && 2 * hotelOrderInfo.Days > hotelOrderInfo.Price - (hotelOrderInfo.Payment / 100 + islijian))
            {
                hotelOrderInfo.Status = 1;
            }
            else
            {
                hotelOrderInfo.Status = 1;
                //订单处理状态 变价
                hotelOrderInfo.ProcStatus = 22;

                //把订单状态改成22（价格有变动）
                //更新订单状态
                SqlParameter[] sp7 = new SqlParameter[6];
                sp7[0] = new SqlParameter("@hoid", SqlDbType.Int);
                sp7[0].Direction = ParameterDirection.Output;
                sp7[1] = new SqlParameter("@orderid", SqlDbType.Int);
                sp7[1].Value = hotelOrderInfo.OrderId;
                sp7[2] = new SqlParameter("@eid", SqlDbType.VarChar);
                sp7[2].Value = "taobao_log";
                sp7[3] = new SqlParameter("@idate", SqlDbType.DateTime);
                sp7[3].Value = DateTime.Now;
                sp7[4] = new SqlParameter("@opclass", SqlDbType.VarChar);
                sp7[4].Value = "价格有变动";
                sp7[5] = new SqlParameter("@opText", SqlDbType.VarChar);
                sp7[5].Value = "淘宝订单酒店价格有变动，注意不要确认";

                try
                {
                    SqlHelper.ExecuteNonQuery("sp3_HotelOrderLog_i", CommandType.StoredProcedure, sp7);
                }
                catch (Exception e)
                {
                    log.Error("添加日志sp3_HotelOrderLog_i异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                    return hotelOrderInfo;
                }
            }

            //更新status订单支付状态
            SqlParameter[] sp3 = new SqlParameter[2];
            sp3[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp3[0].Value = hotelOrderInfo.OrderId;
            sp3[1] = new SqlParameter("@status", SqlDbType.Int);
            sp3[1].Value = hotelOrderInfo.Status;
            try
            {
                SqlHelper.ExecuteNonQuery("sp1_hotelorders_u", CommandType.StoredProcedure, sp3);
            }
            catch (Exception e)
            {
                log.Error("更新订单数据sp1_hotelorders_u异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                return hotelOrderInfo;
            }

            //更新ProcStatus订单处理状态（除了满房与变价，其他默认状态是0）
            SqlParameter[] sp5 = new SqlParameter[2];
            sp5[0] = new SqlParameter("@ProcStatus", SqlDbType.SmallInt);
            sp5[0].Value = hotelOrderInfo.ProcStatus;
            sp5[1] = new SqlParameter("@OrderID", SqlDbType.Int);
            sp5[1].Value = hotelOrderInfo.OrderId;
            try
            {
                SqlHelper.ExecuteNonQuery("sp3_HotelOrders_u", CommandType.StoredProcedure, sp5);
            }
            catch (Exception e)
            {
                log.Error("更新订单数据sp3_HotelOrders_u异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                return hotelOrderInfo;
            }



            SqlParameter[] sp4 = new SqlParameter[1];
            sp4[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp4[0].Value = hotelOrderInfo.OrderId;
            try
            {
                SqlHelper.ExecuteNonQuery("sp1_hotelorders_IsAgent_u", CommandType.StoredProcedure, sp4);
            }

            catch (Exception e)
            {
                log.Error("更新订单数据sp1_hotelorders_IsAgent_u异常hotelId:" + hotelOrderInfo.HotelId + " RoomId:" + hotelOrderInfo.RoomId, e);
                return hotelOrderInfo;
            }
            return hotelOrderInfo;
        }

        /// <summary>
        /// 将支付宝交易号单独保存到一个表供财务使用
        /// </summary>
        /// <param name="hotelOrderInfo"></param>
        private void AddFinanceDate(HotelOrderInfo hotelOrderInfo)
        {
            try
            {
                var dic = new Dictionary<string, object>();
                dic.Add("@OrderId", hotelOrderInfo.OrderId);
                dic.Add("@Oid", hotelOrderInfo.Tid);
                dic.Add("@AlipayTradeNO", hotelOrderInfo.AlipayTradeNO);
                dic.Add("@Created_Time", DateTime.Now);
                SqlParameter[] para = DBHelper.DicToParameters(dic);

                SqlParameter[] para1 = DBHelper.DicToParameters(dic);
                SqlHelper.ExecuteNonQuery(
                    "insert into Taobao_new_FinanceDate(OrderId,Oid,AlipayTradeNO,Created_Time) values(@OrderId,@Oid,@AlipayTradeNO,@Created_Time)",
                    para1, SqlHelper.conStr);
            }

            catch (Exception e)
            {
                log.Error("添加到财务数据异常", e);
            }
        }

        private void Register(HotelOrderInfo hotelOrder)
        {
            try
            {
                UserDAL user = new UserDAL();
                if (string.IsNullOrEmpty(hotelOrder.Mobile))
                {
                    return;
                }
                if (user.CheckMobileIsExist(hotelOrder.Mobile))
                {
                    return;
                }
                UserInfo uinfo = new UserInfo()
                {
                    Email = string.IsNullOrEmpty(hotelOrder.Email) ? "无" : hotelOrder.Email,
                    Gener = string.IsNullOrEmpty(hotelOrder.Gender) ? "M" : hotelOrder.Gender,
                    Mobile = hotelOrder.Mobile,
                    Name = hotelOrder.Contactor,
                    Webfrom = hotelOrder.Webfrom,
                    Userid = "淘宝客户",
                    Pwd = "123456"
                };
                user.Register(uinfo);
            }
            catch (Exception err)
            {
                log.Error("注册会员发生异常," + err.StackTrace);
            }
        }

        #region GetBatchRoomList
        /// <summary>
        /// 获取立减和增幅
        /// </summary>
        /// <param name="hotelId"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public DataTable GetBatchRoomList(int hotelId, int room)
        {
            SqlParameter[] sp = new SqlParameter[2];
            sp[0] = new SqlParameter("@hotelId", SqlDbType.Int);
            sp[0].Value = hotelId;
            sp[1] = new SqlParameter("@room", SqlDbType.Int);
            sp[1].Value = room;
            string sql = @"SELECT LIJIAN,zengfu
                           FROM taobao_new_Product_his AS A
                           INNER JOIN 
                           Hotels AS B
                           ON A.qmg_hotelid =B.hotelid and a.qmg_hotelid=@hotelId and A.qmg_roomid=@room
                           AND B.resourceid != 110
                           ";
            DataTable dt = SqlHelper.ExcuteDataTable(sql, sp, SqlHelper.conStrRead);
            return dt;
        }
        #endregion

        #region GetRound
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="dblnum"></param>
        /// <param name="numberprecision"></param>
        /// <returns></returns>
        public static double GetRound(double dblnum, int numberprecision)
        {
            //Modified by lucky on 2008-11-25 Math.Round(dblnum, numberprecision, MidpointRounding.AwayFromZero) is banker
            int tmpNum = dblnum > 0 ? 5 : -5;
            double dblreturn = Math.Truncate(dblnum * Math.Pow(10, numberprecision + 1)) + tmpNum;
            dblreturn = Math.Truncate(dblreturn / 10) / Math.Pow(10, numberprecision);
            return dblreturn;
        }
        #endregion


        /// <summary>
        /// 请求URl
        /// </summary>
        /// <param name="url"></param>
        private void GetUrl(string url)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Timeout = 5000;
                httpRequest.Method = "GET";
                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                //StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), System.Text.Encoding.GetEncoding("gb2312"));
                //string result = sr.ReadToEnd();
                //result = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                //int status = (int)httpResponse.StatusCode;
                //sr.Close();
            }
            catch (Exception e)
            {
                log.Error("发送邮件短信异常", e);
            }

        }
    }
}
