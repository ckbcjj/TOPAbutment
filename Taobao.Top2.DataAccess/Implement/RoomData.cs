using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Common.Tool;
using Taobao.Top2.Entity.TaobaoEntity;

namespace Taobao.Top2.DataAccess.Implement
{
    class RoomData : IRoomData
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DataTable GetRoomInfoById(string roomId)
        {
            string sql = @"SELECT qhr.room qmg_roomid,
       qhr.roomname roomname,
       qhr.hotelid qmg_hotelid,
       tnr.rid,
       tnh.hid,
       tnh.name hotelname
FROM hotelroom qhr
LEFT JOIN taobao_new_roomtype tnr ON tnr.qmg_roomid=qhr.room
LEFT JOIN taobao_new_hotel tnh ON qhr.hotelid=tnh.qmg_hotelid
WHERE qhr.status = 1
  AND qhr.room =@room";
            return SqlHelper.ExcuteDataTable(sql, new[] { new SqlParameter("@room", roomId) }, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 保存上传成功的酒店房型信息
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool SaveTaobaoRoom(TaobaoRoom room)
        {
            SaveToRoomType(room);
            SaveRoomInfoToHis(room);
            return true;
        }


        /// <summary>
        /// 房型信息保存到Taobao_new_Product_His
        /// </summary>
        /// <param name="room"></param>
        public void SaveRoomInfoToHis(TaobaoRoom room)
        {
            try
            {
                var dic = new Dictionary<string, object>();
                dic.Add("@hid", room.hid);
                dic.Add("@rid", room.rid);
                dic.Add("@hotelid", room.qmg_hotelid);
                dic.Add("@roomid", room.qmg_roomid);
                SqlParameter[] para = DBHelper.DicToParameters(dic);
                int id =
                  Convert.ToInt32(SqlHelper.ExecuteScalar(
                            "select id from dbo.Taobao_new_Product_His where (hid=@hid and rid=@rid) or (qmg_roomid=@roomid and qmg_hotelid=@hotelid)",
                            para, SqlHelper.conStrRead));
                if (id > 0)
                {
                    dic["@id"] = id;
                    SqlParameter[] updatePara = DBHelper.DicToParameters(dic);
                    SqlHelper.ExecuteNonQuery(
                        "update Taobao_new_Product_His set qmg_hotelid=@hotelid,qmg_roomid=@roomid,hid =@hid,rid=@rid where id = @id",
                        updatePara, SqlHelper.conStr);
                }
                else
                {
                    SqlParameter[] para1 = DBHelper.DicToParameters(dic);
                    SqlHelper.ExecuteNonQuery(
                        "insert into Taobao_new_Product_His(qmg_hotelid,qmg_roomid,hid,rid) values(@hotelid,@roomid,@hid,@rid)",
                        para1, SqlHelper.conStr);
                }
            }
            catch (Exception e)
            {
                log.Error("RoomData:SaveRoomInfoToHis 异常", e);
            }
        }

        /// <summary>
        /// 保存到Taobao_new_RoomType表
        /// </summary>
        /// <param name="room"></param>
        public void SaveToRoomType(TaobaoRoom room)
        {
            SqlParameter[] param =
            {
                new SqlParameter("@outerid", room.outer_id)
            };

            int id = Convert.ToInt32(SqlHelper.ExecuteScalar(
                            "select id from dbo.taobao_new_roomtype where outer_id=@outerid",
                            param, SqlHelper.conStrRead));

            if (id <= 0)
            {
                room.created_time = DateTime.Now.ToString();
                room.modified_time = DateTime.Now.ToString();
                string sql = @"INSERT INTO [rest].[dbo].[Taobao_new_RoomType]([qmg_hotelid],[qmg_roomid],[rid] ,[hid],[outer_id] ,outer_hid ,[name] ,[max_occupancy] ,[area],[floor],[bed_type],[bed_size],[internet],[service] ,[window_type],modified_time)
VALUES(@qmg_hotelid,
       @qmg_roomid,
       @rid,
       @hid,
       @outer_id,
       @outHid,
       @name,
       @max_occupancy,
       @area,
       @floor,
       @bed_type,
       @bed_size,
       @internet,
       @service,
       @window_type,
       @modified_time)";
                var dic = DBHelper.ObjectTodic(room);
                var para = DBHelper.DicToParameters(dic);
                SqlHelper.ExecuteNonQuery(sql, para, SqlHelper.conStr);
            }
            else
            {
                room.Id = id;
                room.modified_time = DateTime.Now.ToString();
                var dic = DBHelper.ObjectTodic(room);
                var para = DBHelper.DicToParameters(dic);
                string sql = "update taobao_new_roomtype set name=@name,max_occupancy=@max_occupancy,area=@area,floor=@floor,bed_type=@bed_type,bed_size=@bed_size,internet=@internet,service=@service,window_type=@window_type,modified_time=@modified_time where id=@id";
                SqlHelper.ExecuteNonQuery(sql, para, SqlHelper.conStr);
            }
        }

        public DataTable GetRoomInfoByHotel(string hotel)
        {
            string sql = @"SELECT qhr.room qmg_roomid,
       qhr.roomname roomname,
       qhr.hotelid qmg_hotelid,
       tnr.rid,
       tnh.hid,
       tnh.name hotelname
FROM hotelroom qhr
LEFT JOIN taobao_new_roomtype tnr ON tnr.qmg_roomid=qhr.room
LEFT JOIN taobao_new_hotel tnh ON qhr.hotelid=tnh.qmg_hotelid
WHERE qhr.status = 1
  AND qhr.hotelid =@hotel";
            return SqlHelper.ExcuteDataTable(sql, new[] { new SqlParameter("@hotel", hotel) }, SqlHelper.conStrRead);
        }

        /// <summary>
        /// 获取房型信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="roomId">酒店idt</param>
        /// <returns></returns>
        public DataTable GetRoomInfoByHotelId(int hotelid)
        {
            string sql = @"SELECT qhr.room roomid,tnr.qmg_roomid,tnr.rid,tnr.outer_id, 
       qhr.roomname roomname,
       qhr.hotelid qmg_hotelid,
        qhr.persons,
        qhr.istv,
        qhr.istel,
        qhr.hasDlwy,
        qhr.hotelroom_parentid,
       h.hotelname
FROM hotelroom qhr left join hotels h on  qhr.hotelid = h.hotelid
left join taobao_new_roomtype tnr on qhr.room = tnr.qmg_roomid
WHERE qhr.status = 1 
--and qhr.hotelroom_parentid<>0  
AND qhr.hotelid ={0}";
            return SqlHelper.ExcuteDataTable(string.Format(sql, hotelid), null, SqlHelper.conStrRead);
        }

        public void CleanOffLineData(string rid)
        {
            string sql = "delete taobao_new_roomtype where rid =@rid;"
 + "delete taobao_new_product_his where rid =@rid;";
            SqlParameter[] param =
            {
                new SqlParameter("@rid", rid),
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }

        public DataTable GetRoomTypeStatus()
        {
            string sql = "select * from roomtype_status_log as b where deal=0 and" +
" not exists (select 1 from roomtype_status_log where roomid= b.roomid and b.idate<idate) and ((b.after=-1) or (b.befor=-1 and b.after=1))" +
" and exists (select 1 from taobao_new_product_his ttph where ttph.qmg_roomid=b.roomid)";
            var RoomStatusList = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStr);
            return RoomStatusList;
        }

        public void UpdateTaobaoRoomLogStatus(int roomid)
        {
            string sql = "update roomtype_status_log set deal=1 where roomid=@roomid and deal=0";
            SqlParameter[] param =
            {
                new SqlParameter("@roomid",roomid)
            };
            SqlHelper.ExecuteNonQuery(sql, param, SqlHelper.conStr);
        }
    }
}
