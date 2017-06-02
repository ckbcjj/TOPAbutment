using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Taobao.Top2.Entity.OrderEntity;
using Common.Tool;

namespace Taobao.Top2.DataAccess.Implement
{
    public class UserDAL
    {
        string sql = "";
        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int Register(UserInfo userInfo)
        {
            SqlParameter[] sp = new SqlParameter[16];

            sp[0] = new SqlParameter("@userId", SqlDbType.VarChar, 20);
            sp[0].Value = userInfo.Userid;

            sp[1] = new SqlParameter("@city", SqlDbType.Int);
            sp[1].Value = 0;

            sp[2] = new SqlParameter("@username", SqlDbType.VarChar, 20);
            sp[2].Value = "";

            sp[3] = new SqlParameter("@address", SqlDbType.VarChar, 100);
            sp[3].Value = "";

            sp[4] = new SqlParameter("@zipcode", SqlDbType.Char, 6);
            sp[4].Value = "";

            sp[5] = new SqlParameter("@tel", SqlDbType.VarChar, 100);
            sp[5].Value = "";

            sp[6] = new SqlParameter("@qq", SqlDbType.VarChar, 100);
            sp[6].Value = "";

            sp[7] = new SqlParameter("@msn", SqlDbType.VarChar, 100);
            sp[7].Value = "";

            sp[8] = new SqlParameter("@confirmflag", SqlDbType.VarChar, 20);
            sp[8].Value = "0";

            sp[9] = new SqlParameter("@status", SqlDbType.Int);
            sp[9].Value = 0;

            sp[10] = new SqlParameter("@qianmin", SqlDbType.VarChar, 500);
            sp[10].Value = "";

            sp[11] = new SqlParameter("@webfrom", SqlDbType.VarChar, 200);
            sp[11].Value = userInfo.Webfrom;

            sp[12] = new SqlParameter("@pwd", SqlDbType.VarChar, 20);
            sp[12].Value = userInfo.Pwd;

            sp[13] = new SqlParameter("@mobile", SqlDbType.VarChar, 20);
            sp[13].Value = userInfo.Mobile;

            sp[14] = new SqlParameter("@email", SqlDbType.VarChar, 100);
            sp[14].Value = userInfo.Email;

            sp[15] = new SqlParameter("@gender", SqlDbType.Char, 1);
            sp[15].Value = userInfo.Gener;

            try
            {
                SqlHelper.ExecuteNonQuery("sp1_Users", CommandType.StoredProcedure, sp);
                SqlParameter[] sp1 = new SqlParameter[1];
                sp1[0] = new SqlParameter("@mobile", SqlDbType.VarChar);
                sp1[0].Value = userInfo.Mobile;
                sql = "Select restcard From users  Where mobile=@mobile ";
                DataTable dt = SqlHelper.ExecuteDataTable(sql, sp1);
                if (dt != null && dt.Rows.Count == 1)
                {
                    userInfo.RestCard = dt.Rows[0]["restcard"].ToString();
                    return Convert.ToInt32(userInfo.RestCard);
                }
                return 80018;
            }
            catch (Exception)
            {
                return 80018;
            }
        }

        #region Login
        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool Login(UserInfo userInfo)
        {
            SqlParameter[] sp = new SqlParameter[3];
            sp[0] = new SqlParameter("@rlogins", SqlDbType.VarChar);
            sp[0].Value = userInfo.LoginValue;

            sp[1] = new SqlParameter("@rtype", SqlDbType.VarChar);
            sp[1].Value = userInfo.LoginType;

            sp[2] = new SqlParameter("@pwd", SqlDbType.VarChar);
            sp[2].Value = userInfo.Pwd;

            try
            {
                DataTable dt = SqlHelper.ExecuteDataTable("sp1_users_Checkpwd", CommandType.StoredProcedure, sp);
                if (dt.Rows.Count == 1)
                {
                    userInfo.RestCard = dt.Rows[0]["restcard"].ToString();
                    userInfo.Mobile = GetUserInfo(userInfo.RestCard).Rows[0]["mobile"].ToString();
                    userInfo.Email = GetUserInfo(userInfo.RestCard).Rows[0]["email"].ToString();
                    userInfo.Name = GetUserInfo(userInfo.RestCard).Rows[0]["userName"].ToString();
                    userInfo.Gener = GetUserInfo(userInfo.RestCard).Rows[0]["gender"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region GetPwd
        ///// <summary>
        ///// 找回密码
        ///// </summary>
        ///// <param name="userInfo"></param>
        ///// <returns></returns>
        //public bool GetPwd(UserInfo userInfo)
        //{
        //    SqlParameter[] sp = new SqlParameter[1];

        //    sp[0] = new SqlParameter("@mobile", SqlDbType.VarChar, 20);
        //    sp[0].Value = userInfo.Mobile;

        //    sql = "Select * from  users where mobile=@mobile ";

        //    try
        //    {
        //        DataTable dt = SqlHelper.ExecuteDataTable(sql, sp);
        //        if (dt.Rows.Count == 1)
        //        {
        //            userInfo.RestCard = dt.Rows[0]["restcard"].ToString();
        //            userInfo.Mobile = GetUserInfo(userInfo.RestCard).Rows[0]["mobile"].ToString();
        //            //userInfo.Pwd = GetUserInfo(userInfo.RestCard).Rows[0]["pwd"].ToString();               

        //            //生成一个新的随机密码，并加密保存到数据库

        //            string newPwd = QMangoSys.Common.Utils.GetNumPwd(6);
        //            userInfo.Pwd = newPwd;
        //            SqlParameter[] spCPWD = new SqlParameter[2];

        //            spCPWD[0] = new SqlParameter("@restCard", SqlDbType.Int);
        //            spCPWD[0].Value = userInfo.RestCard;

        //            spCPWD[1] = new SqlParameter("@pwd", SqlDbType.VarChar);
        //            spCPWD[1].Value = QMangoSys.Common.Utils.MD5(newPwd);

        //            SqlHelper.ExecuteDataTable("sp3_users_u", CommandType.StoredProcedure, spCPWD);

        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        #endregion

        #region GetUserInfo 获取用户资料
        /// <summary>
        /// 根据卡号获取用户信息
        /// </summary>
        /// <param name="restcard">卡号</param>
        /// <returns></returns>
        public DataTable GetUserInfo(string restcard)
        {
            SqlParameter[] sp = new SqlParameter[1];

            sp[0] = new SqlParameter("@restcard", SqlDbType.VarChar, 20);
            sp[0].Value = restcard;

            sql = "Select * from  users where restcard=@restcard ";
            return SqlHelper.ExecuteDataTable(sql, sp);
        }
        #endregion

        /// <summary>
        /// 根据卡号获取会员资金余额
        /// </summary>
        /// <param name="restcard"></param>
        /// <returns></returns>
        public int GetUserBank(string restcard)
        {
            SqlParameter[] sp = new SqlParameter[1];

            sp[0] = new SqlParameter("@restcard", SqlDbType.Int);
            sp[0].Value = restcard;

            DataTable dt = SqlHelper.ExecuteDataTable("sp5_GetUserBank_New", CommandType.StoredProcedure, sp);

            return Convert.ToInt32(Convert.ToDecimal(dt.Rows[0]["ubank"].ToString()));
        }


        #region UserKK
        /// <summary>
        /// 扣款
        /// </summary>
        /// <param name="restcard">扣款会员卡号</param>
        /// <param name="ysprice">客人应付金额</param>
        /// <param urpOrderAmount="urpOrderAmount">抵扣红包</param>
        /// <param name="orderid">需扣款的订单号</param>
        /// <returns></returns>
        public bool UserKK(string restcard, string ysprice, string urpOrderAmount, string orderid)
        {
            if (restcard == "" || ysprice == "" || urpOrderAmount == "" || orderid == "")
            {
                return false;
            }
            double kk_amount = 0.00;
            double ff_amount = 0.00;
            double amount = Convert.ToDouble(ysprice) - Convert.ToDouble(urpOrderAmount);
            double orderamount = 0.00;
            #region
            //using (SqlConnection conn=new SqlConnection(SQLSession.Session.ConnectionString))
            //{
            //    conn.Open();
            //    using(SqlTransaction trans=conn.BeginTransaction())
            //    {
            //        try
            //        {
            //            SqlParameter[] sp = new SqlParameter[1];
            //            sp[0] = new SqlParameter("@restcard", SqlDbType.Int);
            //            sp[0].Value = restcard;
            //            sql = "select restcard,sum(fsamount) as fsamount,trade_no,oorderid from UserOrderBank where restcard=@restcard group by restcard,trade_no,oorderid having sum(fsamount)>0";
            //            DataTable dt = SQLSession.Session.ExecuteDataTable(sql,sp);
            //            if (dt.Rows.Count > 0)
            //            {
            //                double fsamount = 0.00;
            //                foreach (DataRow dr in dt.Rows)
            //                {
            //                    fsamount = Convert.ToDouble(dr["fsamount"].ToString());
            //                    if (amount > kk_amount)
            //                    {
            //                        if (amount - kk_amount <= fsamount)
            //                        {
            //                            kk_amount = amount - kk_amount;
            //                            ff_amount = ff_amount + kk_amount;
            //                            SqlParameter[] sp1 = new SqlParameter[6];
            //                            sp1[0] = new SqlParameter("@orderid", SqlDbType.Int);
            //                            sp1[0].Value = orderid;
            //                            sp1[1] = new SqlParameter("@OorderID", SqlDbType.Int);
            //                            sp1[1].Value = dr["oorderid"];
            //                            sp1[2] = new SqlParameter("@trade_no", SqlDbType.VarChar);
            //                            sp1[2].Value = dr["trade_no"];
            //                            sp1[3] = new SqlParameter("@restcard", SqlDbType.Int);
            //                            sp1[3].Value = restcard;
            //                            sp1[4] = new SqlParameter("@fsamount", SqlDbType.VarChar);
            //                            sp1[4].Value = kk_amount;
            //                            sp1[5] = new SqlParameter("@temp_amount", SqlDbType.Int);
            //                            sp1[5].Value = 0;
            //                            SQLSession.Session.ExecuteNonQuery("sp5_UserOrderBank_KK", CommandType.StoredProcedure, sp1);

            //                            //自动解除hold状态

            //                            SqlParameter[] sp2 = new SqlParameter[2];
            //                            sp2[0] = new SqlParameter("@trade_no", SqlDbType.Int);
            //                            sp2[0].Value = dr["trade_no"];
            //                            sp2[1] = new SqlParameter("@status", SqlDbType.Int);
            //                            sp2[1].Value = 0;
            //                            SQLSession.Session.ExecuteNonQuery("sp5_NoAutoRefund_u", CommandType.StoredProcedure, sp1);
            //                            break; 
            //                        }
            //                        else
            //                        {
            //                            kk_amount = kk_amount + fsamount;
            //                            ff_amount = kk_amount;

            //                            SqlParameter[] sp1 = new SqlParameter[6];
            //                            sp1[0] = new SqlParameter("@orderid", SqlDbType.Int);
            //                            sp1[0].Value = orderid;
            //                            sp1[1] = new SqlParameter("@OorderID", SqlDbType.Int);
            //                            sp1[1].Value = dr["oorderid"];
            //                            sp1[2] = new SqlParameter("@trade_no", SqlDbType.VarChar);
            //                            sp1[2].Value = dr["trade_no"];
            //                            sp1[3] = new SqlParameter("@restcard", SqlDbType.Int);
            //                            sp1[3].Value = restcard;
            //                            sp1[4] = new SqlParameter("@fsamount", SqlDbType.VarChar);
            //                            sp1[4].Value = fsamount;
            //                            sp1[5] = new SqlParameter("@temp_amount", SqlDbType.Int);
            //                            sp1[5].Value = amount - kk_amount;
            //                            SQLSession.Session.ExecuteNonQuery("sp5_UserOrderBank_KK", CommandType.StoredProcedure, sp1);

            //                            //自动解除hold状态

            //                            SqlParameter[] sp2 = new SqlParameter[2];
            //                            sp2[0] = new SqlParameter("@trade_no", SqlDbType.Int);
            //                            sp2[0].Value = dr["trade_no"];
            //                            sp2[1] = new SqlParameter("@status", SqlDbType.Int);
            //                            sp2[1].Value = 0;
            //                            SQLSession.Session.ExecuteNonQuery("sp5_NoAutoRefund_u", CommandType.StoredProcedure, sp1);
            //                        }
            //                    }
            //                }
            //            }
            //            trans.Commit();
            //        }
            //        catch
            //        {
            //            trans.Rollback();
            //            return false;
            //        }
            //    }
            //}
            //orderamount = ff_amount;
            #endregion
            orderamount = 0.00;
            double recamount = 0.00;
            if (Convert.ToDouble(ysprice) - Convert.ToDouble(urpOrderAmount) > orderamount)
            {
                recamount = UserRecBank_KK(restcard, amount.ToString(), orderid, "rec");
                if (recamount == -1)
                {
                    return false;
                }
            }
            if (Convert.ToDouble(ysprice) - Convert.ToDouble(urpOrderAmount) - orderamount > recamount)
            {
                double tempAmout = Convert.ToDouble(ysprice) - Convert.ToDouble(urpOrderAmount) - orderamount - recamount;
                if (IsAgent(restcard))
                {
                    if (UserRecBank_KK(restcard, tempAmout.ToString(), orderid, "com") == -1)
                    {
                        return false;
                    }
                }
                else
                {
                    if (UserRecBank_KK(restcard, tempAmout.ToString(), orderid, "bon") == -1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region UserRecBank_KK
        /// <summary>
        /// 扣除充值/奖金/佣金账户
        /// </summary>
        /// <param name="restcard"></param>
        /// <param name="amount"></param>
        /// <param name="orderid"></param>
        /// <param name="ac"></param>
        private double UserRecBank_KK(string restcard, string amount, string orderid, string ac)
        {
            double recamount = 0.00;
            SqlParameter[] sp1 = new SqlParameter[3];
            sp1[0] = new SqlParameter("@orderid", SqlDbType.Int);
            sp1[0].Value = orderid;
            sp1[1] = new SqlParameter("@restcard", SqlDbType.Int);
            sp1[1].Value = restcard;
            sp1[2] = new SqlParameter("@fsamount", SqlDbType.VarChar);
            sp1[2].Value = amount;
            string procName = "";
            if (ac == "rec")
            {
                procName = "sp5_UserRecBank_KK";
            }
            else if (ac == "com")
            {
                procName = "sp5_UserComBank_KK";
            }
            else if (ac == "bon")
            {
                procName = "sp5_UserBonBank_KK";
            }
            else
            {
                procName = "sp5_UserRecBank_KK";
            }

            using (SqlConnection conn = new SqlConnection(SqlHelper.conStr))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        DataTable dt = SqlHelper.ExecuteDataTable(procName, CommandType.StoredProcedure, sp1);
                        recamount = Convert.ToDouble(dt.Rows[0]["yxamount"].ToString());
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return recamount;
        }
        #endregion


        #region CheckMobile 检查手机是否存在

        public bool CheckMobileIsExist(string mobile)
        {
            SqlParameter[] sp = new SqlParameter[1];

            sp[0] = new SqlParameter("@mobile", SqlDbType.VarChar, 20);
            sp[0].Value = mobile;

            sql = "Select count(1) From Users Where mobile=@mobile ";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(sql, sp));
            if (count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region CheckUserIsExist 检查会员是否存在

        //public bool CheckUserIsExist(string  restcard)
        //{
        //    SqlParameter[] sp = new SqlParameter[1];



        //    sp[0] = new SqlParameter("@restcard", SqlDbType.BigInt);
        //    sp[0].Value = restcard;

        //    sql = "Select Count(*) From Users Where restcard=@restcard ";
        //    int recordCount = SqlHelper.ExecuteScalar(sql, sp);
        //    if (recordCount > 0)
        //        return true;
        //    else
        //        return false;
        //}
        #endregion

        #region IsAgent
        /// <summary>
        /// 检查会员是否是直销代理
        /// </summary>
        /// <param name="restcard"></param>
        /// <returns></returns>
        public bool IsAgent(string restcard)
        {
            SqlParameter[] sp = new SqlParameter[1];

            sp[0] = new SqlParameter("@restcard", SqlDbType.BigInt);
            sp[0].Value = restcard;

            sql = "Select Count(*) From  qagent where agenttype in (1,4) and  restcard=@restcard ";
            object recordCount = SqlHelper.ExecuteScalar(sql, sp);
            if (int.Parse(recordCount.ToString()) > 0)
                return true;
            else
                return false;
        }
        #endregion


        #region IsAgent 检查会员是否是代理
        /// <summary>
        /// 检查会员是否是代理
        /// </summary>
        /// <param name="restcard"></param>
        /// <returns></returns>
        public bool IsAgentUser(string restcard)
        {
            SqlParameter[] sp = new SqlParameter[1];

            sp[0] = new SqlParameter("@restcard", SqlDbType.BigInt);
            sp[0].Value = restcard;

            sql = "Select Count(*) From  qagent where status=1 and restcard=@restcard ";
            object recordCount = SqlHelper.ExecuteScalar(sql, sp);
            if (int.Parse(recordCount.ToString()) > 0)
                return true;
            else
                return false;
        }
        #endregion
    }
}
