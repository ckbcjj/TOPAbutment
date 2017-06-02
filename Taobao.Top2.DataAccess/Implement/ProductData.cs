using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common.Tool;
using Taobao.Top2.Entity;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess.Implement
{
    public class ProductData : IProductData
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetTaobaoRoomType(List<string> hotels)
        {
            return SqlHelper.ExcuteDataTable(@"SELECT p.provincename,tnph.*,hr.*,h.hotelname
FROM dbo.Taobao_new_Product_His tnph
LEFT JOIN hotelroom hr ON hr.room = tnph.qmg_roomid
LEFT JOIN hotels h ON h.hotelid=tnph.qmg_hotelid
LEFT JOIN province p ON p.province=h.province
WHERE tnph.rid is null and tnph.qmg_hotelid in(" + string.Join(",", hotels) + ")", null, SqlHelper.conStrRead);

        }

        /// <summary>
        /// 获取需要下线的数据
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="hotelIds"></param>
        /// <returns></returns>
        public DataTable GetDownList(List<int> rooms, int[] hotelIds = null)
        {
            string sql = null;
            if (rooms == null)
            {
                sql = "select * from taobao_new_Product_his where qmg_roomid is not null and gid is not null";
            }
            else
            {
                sql = "select * from taobao_new_Product_his where qmg_roomid is not null and gid is not null and qmg_roomid in(" + string.Join(",", rooms) + ")";
            }

            if (hotelIds != null)
            {
                sql += " and qmg_hotelid in (" + string.Join(",", hotelIds) + ")";
            }
            else
            {
                sql = "";
            }
            return SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
        }


        /// <summary>
        /// 获取全量更新数据(包含对指定一些酒店全量更新)
        /// </summary>
        /// <param name="rooms"></param>
        /// <param name="hotelIds"></param>
        /// <returns></returns>
        public DataTable GetUpdateList(int[] hotelIds = null)
        {
            string sql = "select tnph.*,tnr.outer_id from taobao_new_Product_his tnph left join taobao_new_roomtype tnr on tnph.rid = tnr.rid where tnph.hstatus=0 and tnph.rstatus=0";
            if (hotelIds != null)
            {
                sql += " and tnph.qmg_hotelid in (" + string.Join(",", hotelIds) + ")";
            }
            return SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 获取90天的价格计划
        /// </summary>
        /// <param name="roomid"></param>
        /// <returns></returns>
        public DataTable GetRoomStatus(string roomid, double lijianRate, double zengfuRate)
        {
            string sql = @"select effectdate,eprice,(ebeds-aviebeds) as num,commission,"+ lijianRate + @" as lijian,"+ zengfuRate + @" as zengfu,h.resourceid,h.hotelid,h.hotelname,h.huserid
  from hotelprice hp
LEFT JOIN HotelRoom AS hr ON hr.room = hp.room
LEFT JOIN hotels h ON h.hotelid = hr.hotelid
  where hp.room=" + roomid + @"
and hp.effectdate>=CONVERT(varchar(10),getdate(),120) and hp.effectdate<dateadd(d,59,getdate()) 
and hp.eprice>=0";
            DataTable roomdatatable = null;
            try
            {
                roomdatatable = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
            }
            catch (Exception ex)
            {
                log.Error("ProductData:GetRoomStatus 异常", ex);
                roomdatatable = null;
            }

            return roomdatatable;
        }

        /// <summary>
        /// 更新rpid
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <param name="rpid"></param>
        public void UpdateRpId(int hotelid, int roomid, string rpid)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@hotelid", hotelid),
                new SqlParameter("@roomid", roomid),
                new SqlParameter("@rpid", rpid)
            };
            string sql =
                "update Taobao_new_Product_His set rpid=@rpid where qmg_hotelid = @hotelid and qmg_roomid=@roomid";
            SqlHelper.ExecuteNonQuery(sql, para, SqlHelper.conStr);
        }

        /// <summary>
        /// 更新gid
        /// </summary>
        /// <param name="hotelid"></param>
        /// <param name="roomid"></param>
        /// <param name="gid"></param>
        public void UpdateGId(int hotelid, int roomid, string gid)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@hotelid", hotelid),
                new SqlParameter("@roomid", roomid),
                new SqlParameter("@gid", gid)
            };
            string sql =
                "update Taobao_new_Product_His set gid=@gid where qmg_hotelid = @hotelid and qmg_roomid=@roomid";
            SqlHelper.ExecuteNonQuery(sql, para, SqlHelper.conStr);
        }

        public DataTable GetIncrPrice(DateTime begintime,DateTime endtime)
        {
            string sql = @"SELECT hp.roompriceid,hp.effectDate,hp.ePrice,hp.commission,(hp.eBeds-hp.avieBeds) num,tnph.lijian,tnph.zengfu,tnph.gid,tnph.rpid,tnph.rateplan_code,tnph.qmg_hotelid,tnph.qmg_roomid,h.huserid,h.hotelname,h.resourceid,tnr.outer_id FROM hotelprice hp
LEFT JOIN Taobao_new_Product_His AS tnph ON hp.room = tnph.qmg_roomid left join taobao_new_roomtype tnr on tnph.rid = tnr.rid 
LEFT JOIN hotels h ON h.hotelid = tnph.qmg_hotelid
WHERE tnph.id IS NOT NULL 
 AND hp.lasttime >=@begintime 
 AND hp.lasttime <=@endtime
 and hp.effectdate<dateadd(d,59,getdate())
 --AND hp.lasttime >'2017-03-20 16:56:05.003'
 AND tnph.gid IS not NULL
 --and qmg_hotelid=161307
 --and tnph.qmg_roomid=1253167
 and tnph.hstatus=0 and tnph.rstatus=0";

            SqlParameter[] para =
            {
                new SqlParameter("@begintime",begintime),
                new SqlParameter("@endtime",endtime)
            };

            DataTable roomdatatable = null;
            try
            {
                roomdatatable = SqlHelper.ExcuteDataTable(sql, para, SqlHelper.conStrRead);
                log.Info(string.Format("增量获取数据行:{0},时间段{1}—{2}", roomdatatable.Rows.Count, begintime.ToString("HH:mm:ss"), endtime.ToString("HH:mm:ss")));
            }
            catch (Exception ex)
            {
                log.Error("ProductData:GetIncrPrice 异常", ex);
                roomdatatable = null;
            }

            return roomdatatable;

        }

        /// <summary>
        /// 获取酒店房型信息
        /// </summary>
        /// <param name="roomids"></param>
        /// <returns></returns>
        public DataTable GetRoomInfoByRoom(int roomid)
        {
            return SqlHelper.ExcuteDataTable(@"select p.provincename,hr.*,h.hotelname,h.resourceid from hotelroom hr left join hotels h ON 
h.hotelid=hr.hotelid LEFT JOIN province p ON
 p.province=h.province WHERE hr.room="+roomid, null, SqlHelper.conStrRead);
        }

        public DataTable GetTaobaoRoomTypeByRid(string rid)
        {
            return SqlHelper.ExcuteDataTable(@"select tnr.qmg_roomid,tnph.gid from taobao_new_roomtype tnr left join taobao_new_product_his tnph on tnr.qmg_roomid=tnph.qmg_roomid where tnr.rid='" + rid + "'", null, SqlHelper.conStr);
        }

        public bool UpdataProductHis(TaobaoProduct product)
        {
            try
            {
                SqlParameter[] para =
            {
                new SqlParameter("@rpid", product.rpid),
            };
                string sql = "select count(1) from taobao_new_product_his where rpid=@rpid";
                int id = (int)(SqlHelper.ExecuteScalar(sql, para, SqlHelper.conStrRead) ?? 0);
                if (id == 0)
                {
                    var dic2 = DBHelper.ObjectTodic(product);
                    SqlParameter[] para2 = DBHelper.DicToParameters(dic2);
                    SqlHelper.ExecuteNonQuery(
                        "insert into Taobao_new_Product_His(qmg_hotelid,qmg_roomid,hid,rid,rpid,gid,rateplan_code,zengfu,lijian) values(@qmg_hotelid,@qmg_roomid,@hid,@rid,@rpid,@gid,@rateplan_code,@zengfu,@lijian)",
                        para2, SqlHelper.conStr);
                }
                else
                {
                    product.modified_time = DateTime.Now;
                    var dic3 = DBHelper.ObjectTodic(product);
                    SqlParameter[] para3 = DBHelper.DicToParameters(dic3);
                    SqlHelper.ExecuteNonQuery(
                        "update Taobao_new_Product_His set modified_time=@modified_time where rpid=@rpid",
                        para3, SqlHelper.conStr);
                }
                return true;
            }
            catch (Exception err)
            {
                log.Error("UpdataProductHis出错" + err);
                return false;
            }
        }

        public void UpdateGId(string hid, string rid, long rpid, string gid)
        {
            SqlParameter[] para =
            {
                new SqlParameter("@hid", hid),
                new SqlParameter("@rid", rid),
                new SqlParameter("@rpid", rpid.ToString()), 
                new SqlParameter("@gid", gid)
            };
            SqlHelper.ExecuteNonQuery("update Taobao_new_Product_His set gid=@gid where hid=@hid,rid=@rid,rpid=@rpid",
                para, SqlHelper.conStr);
        }

        public void CleanOffLineData(string rpid)
        {
            if (!string.IsNullOrEmpty(rpid))
            {
                string sql = "delete taobao_new_product_his where rpid =@rpid;";
                SqlParameter[] param =
            {
                new SqlParameter("@rpid", rpid),
            };
                SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
            }
        }

        public DataTable GetRpidListByRoomId(int roomid)
        {
            string sql = "select rpid from taobao_new_product_his where qmg_roomid =" + roomid;
            return SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
        }

        public DataTable GetRpidListByHotelId(int hotelid)
        {
            string sql = "select rpid,hid from taobao_new_product_his where qmg_hotelid =" + hotelid;
            return SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
        }

        public void UpdateProductStatusByHotelId(int hotelid, bool online)
        {
            string sql = "update taobao_new_product_his set hstatus=@hstatus where qmg_hotelid=@hotelid";
            string hstatus = online ? "0" : "-1";
            SqlParameter[] param =
            {
                new SqlParameter("@hstatus",hstatus),
                new SqlParameter("@hotelid",hotelid)
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }

        public void UpdateProductStatusByRoomId(int roomid, bool online)
        {
            string sql = "update taobao_new_product_his set rstatus=@rstatus where qmg_roomid=@roomid";
            string rstatus = online ? "0" : "-1";
            SqlParameter[] param =
            {
                new SqlParameter("@rstatus",rstatus),
                new SqlParameter("@roomid",roomid)
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }
    }
}
