using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Common.Tool;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess.Implement
{
    class HotelData : IHotelData
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataTable GetHotelById(string hotelid)
        {
            string str = string.Join(",", hotelid);
            string format = @"SELECT ts.province p2,
       ts.city city2,
       hs.* from
  (SELECT CASE WHEN c1.cpid=0 THEN c1.city ELSE c1.cpid END city,
		 CASE WHEN c1.cpid=0 THEN c1.cityname ELSE c2.cityname END cityname, 
			h.hotelname,h.hotelid, p.provincename, h.address,h.description,h.hotellogo,h.province
   FROM hotels h
   LEFT JOIN city c1 ON h.city = c1.city
   LEFT JOIN city c2 ON c1.cpid = c2.city
   LEFT JOIN province p ON p.province = c1.province
   WHERE h.status IN (1,4)
     AND h.hotellogo <>''
     AND h.hotelid ={0} hs
LEFT JOIN
  (SELECT DISTINCT province,
                   provincename,
                   city,
                   cityname
   FROM taobao_city) ts ON hs.provincename=ts.provincename
AND ts.cityname LIKE '%' + hs.cityname + '%'";
            return SqlHelper.ExcuteDataTable(string.Format(format, str), null, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 获取酒店信息
        /// </summary>
        /// <param name="hotelid">酒店ID列表</param>
        /// <returns></returns>
        public DataTable GetHotelById(List<int> hotelid)
        {
            try
            {
                string str = string.Join(",", hotelid);
                string format = @"SELECT ts.province p2,
       ts.city city2,
       hs.* from
  (SELECT CASE WHEN c1.cpid=0 THEN c1.city ELSE c1.cpid END city,
		 CASE WHEN c1.cpid=0 THEN c1.cityname ELSE c2.cityname END cityname, 
			h.hotelname,h.hotelid, p.provincename,'13700000000' as mobile, h.address,h.description,h.hotellogo,h.province,h.hotel_parentid,h.isdefaulthotel,th.qmg_hotelid,th.outer_id,th.hid    
   FROM hotels h
   LEFT JOIN city c1 ON h.city = c1.city
   LEFT JOIN city c2 ON c1.cpid = c2.city
   LEFT JOIN province p ON p.province = c1.province
   LEFT JOIN TAOBAO_New_HOTEL TH ON TH.QMG_HOTELID = H.HOTELID
   WHERE h.status IN (1,4) 
     AND h.hotelid IN ({0})) hs
LEFT JOIN
 (SELECT DISTINCT province,
                  provincename,
                   city,
                   cityname,areaname
   FROM taobao_city) ts ON 
(charindex(hs.provincename,ts.provincename)>0 or charindex(ts.provincename,hs.provincename)>0)
AND 
(
charindex(hs.cityname,ts.cityname)>0 or charindex(ts.cityname,hs.cityname)>0
--OR charindex(hs.cityname,ts.areaname)>0 or charindex(ts.areaname,hs.cityname)>0  --进一步按照城市名称区域名称匹配
)";
                return SqlHelper.ExcuteDataTable(string.Format(format, str), null, SqlHelper.conStrRead);
            }
            catch (Exception err)
            {
                log.Error(err);
                return null;
            }
        }

    /// <summary>
    /// 保存酒店信息
    /// </summary>
    /// <param name="taobao">酒店实体</param>
    /// <returns></returns>
    public bool SaveHotels(TaobaoHotel taobao)
        {
            try
            {
                var dic = DBHelper.ObjectTodic(taobao);
                var param = DBHelper.DicToParameters(dic);
                string sql = "INSERT INTO [rest].[dbo].[Taobao_new_Hotel]([hid],[outer_id],[name],[domestic],[country],[province],[city],[district],[business],[address],[longitude],[latitude],[position_type],[tel],[extend],[qmg_HotelId],[s_hotel],[Remark])\r\nVALUES(@hid,@outer_id,@name,@domestic,@country,@province,@city,@district,@business,@address,@longitude,@latitude,@position_type,@tel,@extend,@qmg_HotelId,@s_hotel,@Remark)";
                SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable GetHotelsByCondition(Dictionary<string, object> dic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT top 40 qh.HotelId, qh.HotelName,tnh.hid,(case when hid is null then 0 else 1 end) status
FROM HOTELS qh
LEFT JOIN taobao_new_hotel tnh ON QH.HOTELID=TNH.QMG_HOTELID
LEFT JOIN hotelclass hc ON qh.hotelclassid=hc.hotelclassid
WHERE (qh.status=1 OR qh.status=4) ");
            if (dic.ContainsKey("Status") && (int)dic["Status"] >= 0)
            {
                if ((int)dic["Status"] == 1)
                {
                    sb.Append("and hid is null ");
                }
                else
                {
                    sb.Append("and hid is not null ");
                }
            }
            if (dic.ContainsKey("HotelIds") && !string.IsNullOrEmpty((string)dic["HotelIds"]))
            {
                sb.Append("and qh.hotelid in (").Append(dic["HotelIds"]).Append(") ");
            }
            if (dic.ContainsKey("HotelName") && !string.IsNullOrEmpty((string)dic["HotelName"]))
            {
                sb.Append("and qh.hotelName like '%").Append(dic["HotelName"]).Append("%' ");
            }

            return SqlHelper.ExcuteDataTable(sb.ToString(), null, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 判断酒店是否上传
        /// </summary>
        /// <param name="qmgHotelId"></param>
        /// <returns></returns>
        public string GetTaobaoHotel(int qmgHotelId)
        {
            try
            {
                string selectSql = "select hid from taobao_new_hotel where qmg_HotelId  = " + qmgHotelId;
                return SqlHelper.ExecuteScalar(selectSql, null, SqlHelper.conStrRead).ToString();
            }
            catch
            {
            }
            return null;
        }

        public DataTable GetHidByHotelId(int[] hotelids)
        {
            string sql = "select hid,qmg_hotelid,outer_id from taobao_new_hotel where qmg_hotelid in(" + string.Join(",", hotelids) + ")";
            return SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
        }

        public DataTable TaoBaoHotelSearch(string hid)
        {
            string sql = "select * from taobao_new_hotel where hid=@hid";
            SqlParameter[] para = { new SqlParameter("@hid", hid) };
            return SqlHelper.ExcuteDataTable(sql, para, SqlHelper.conStrRead);

        }

        public DataTable TaoBaoHotelSearch(int hotelid)
        {
            string sql = "select * from taobao_new_hotel where qmg_hotelid=@hotelid";
            SqlParameter[] para = { new SqlParameter("@hotelid", hotelid) };
            return SqlHelper.ExcuteDataTable(sql, para, SqlHelper.conStrRead);
        }

        public void UpDateTaobaoHotel(TaobaoHotel taobaoHotel)
        {
            string sql = "select id from taobao_new_hotel where outer_id=@outer_id";
            SqlParameter[] parameters = { new SqlParameter("@outer_id", taobaoHotel.outer_id) };
            int id = (int)(SqlHelper.ExecuteScalar(sql, parameters, SqlHelper.conStrRead) ?? 0);
            if (id <= 0)
            {
                SaveHotels(taobaoHotel);
            }
            else
            {
                taobaoHotel.modified_time = DateTime.Now;
                taobaoHotel.Id = id;
                UpdateTaobaoHotelById(taobaoHotel);
            }
        }

        private static void UpdateTaobaoHotelById(TaobaoHotel taobao)
        {
            string sql = @"UPDATE [rest].[dbo].[Taobao_new_Hotel]
   SET modified_time=@modified_time,name=@name,country=@country,province=@province,city=@city,address=@address,tel=@tel   					
   WHERE id=@id";
            var dic = DBHelper.ObjectTodic(taobao);
            var param = DBHelper.DicToParameters(dic);
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }


        public void CleanOffLineData(string hid)
        {
            string sql = "delete taobao_new_hotel where hid =@hid;"
 + "delete taobao_new_roomtype where hid =@hid;"
 + "delete taobao_new_product_his where hid =@hid;";
            SqlParameter[] param =
            {
                new SqlParameter("@hid", hid),
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }

        public void CleanOffLineDataByHotelId(int hotelid)
        {
            string sql = "delete taobao_new_hotel where qmg_hotelid =@hotelid;"
 + "delete taobao_new_roomtype where qmg_hotelid =@hotelid;"
 + "delete taobao_new_product_his where qmg_hotelid =@hotelid;";
            SqlParameter[] param =
            {
                new SqlParameter("@hotelid", hotelid),
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }

        public DataTable GetHotelStatus()
        {
            string sql = "select * from hotels_status_log as b where deal=0 and" +
" not exists (select 1 from hotels_status_log where hotelid= b.hotelid and b.idate<idate) and ((b.after=-1) or (b.befor=-1 and b.after=1))" +
" and exists (select 1 from taobao_new_product_his tnph where tnph.qmg_hotelid=b.hotelid)";
            var HotelStatusList = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStr);
            return HotelStatusList;
        }

        public void UpdateTaobaoHotelStatus(int hotelid, bool online)
        {
            string sql = "update taobao_new_product_his set hstatus=@hstatus where qmg_hotelid =@hotelid;";
            sql += "update hotels_status_log set deal=1 where hotelid=@hotelid and deal=0";
            string hstatus = online ? "0" : "-1";
            SqlParameter[] param =
            {
                new SqlParameter("@hstatus",hstatus),
                new SqlParameter("@hotelid",hotelid)
            };
            var HotelStatusList = SqlHelper.ExcuteDataTable(sql, param, SqlHelper.conStr);
        }

        public void UpdateTaobaoHotelLogStatus(int hotelid)
        {
            string sql = "update hotels_status_log set deal=1 where hotelid=@hotelid and deal=0";
            SqlParameter[] param =
            {
                new SqlParameter("@hotelid",hotelid)
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }

        public DataTable GetTaoBaoHotel(int hotelid)
        {
            string sql = "select outer_id,hid from taobao_new_hotel where qmg_hotelid=@hotelid";
            SqlParameter[] param =
            {
                new SqlParameter("@hotelid",hotelid)
            };
            return SqlHelper.ExcuteDataTable(sql, param, SqlHelper.conStrRead);
        }
    }
}
