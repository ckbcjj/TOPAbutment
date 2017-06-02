using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common.Tool;
using Top.Api.Request;
using System.Threading.Tasks;
using Taobao.Top2.Application.Common;
using Taobao.Top2.Application.Implement;
using Taobao.Top2.DataAccess;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;

namespace Taobao.Top2.Application
{
    public class CleanData
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static int CleanTaobaoDbErroData()
        {
            int i = 0;
            IHotelData HotelData = DataFactory.CreateHotelData();
            string sql = "select distinct(hid) from taobao_new_hotel";
            DataTable dt = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStr);
            log.Info("获取数据条数:" + dt.Rows.Count);
            Parallel.ForEach(dt.Select(), new ParallelOptions { MaxDegreeOfParallelism = 5 }, dr =>
            {
                try
                {
                    i++;
                    log.Info(i);
                    XhotelGetRequest getHotel = new XhotelGetRequest();
                    getHotel.Hid = long.Parse(dr["hid"].ToString());
                    var res = TopConfig.Execute(getHotel);
                    if (res.IsError && res.SubErrCode == "isv.invalid-parameter:HOTEL_NOT_EXIST")
                    {
                        HotelData.CleanOffLineData(getHotel.Hid.ToString());
                        log.Info("清理酒店，hid:" + getHotel.Hid.ToString());
                    }
                }
                catch (Exception er)
                {
                    log.Error("异常hid:"+dr["hid"].ToString()+"，" + er.Message);
                }
            });
            return i;
        }
    }

    public class CheckHotels
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        const string filepath1 = @"C:\Users\qmango\Desktop\酒店检查\删除（本地库不存在）.txt";
        const string filepath2 = @"C:\Users\qmango\Desktop\酒店检查\删除（多供应商，留一）.txt";
        public static void CheckTaobaoHotelInfo()
        {
            List<string> lst1 = new List<string>();
            List<string> lst2 = new List<string>();
            string filePath = @"C:\Users\qmango\Desktop\全量卖家酒店列表 (1) - 副本.txt";
            List<Tuple<long, string>> hotelHids = GetHotelIdList(filePath);
            if (hotelHids != null && hotelHids.Count() != 0)
            {
                Parallel.ForEach(hotelHids, new ParallelOptions { MaxDegreeOfParallelism = 15 }, hid =>
                {
                    string sql = "select count(1) from taobao_new_hotel where hid='" + hid.Item1 + "'";
                    if (SqlHelper.ExecuteScalar(sql, null, SqlHelper.conStrRead).ToString() == "0")
                    {
                        DeleteInfo1(lst1, hid);
                        return;
                    }
                    if (IsHotelExist(hid.Item1.ToString()))
                    {
                        if (CheckIsOnLine(hid.Item1.ToString()))
                        {
                            DeleteInfo2(lst2, hid);
                        }
                    }
                });
                File.AppendAllText(filepath1, string.Join("\r\n", lst1));
                File.AppendAllText(filepath2, string.Join("\r\n", lst2));
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void DeleteInfo1(List<string> lst, Tuple<long, string> hid)
        {
            if (lst.FirstOrDefault(t=>t.Contains(hid.Item1.ToString()))==null)
            {
                lst.Add(string.Format("{0},{1}",hid.Item1,hid.Item2));
                log.Info("删除(本地库不存在),hid:" + hid.Item1);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private static void DeleteInfo2(List<string> lst, Tuple<long, string> hid)
        {
            if (lst.FirstOrDefault(t => t.Contains(hid.Item1.ToString())) == null)
            {
                lst.Add(string.Format("{0},{1}", hid.Item1, hid.Item2));
                log.Info("删除(多供应商，留一),hid:" + hid.Item1);
            }
        }

        private static bool CheckIsOnLine(string hid)
        {
            string sql = "select hid from taobao_new_hotel where qmg_hotelid in(select hotelid from hotels where hotel_parentid in (select h.hotel_parentid from taobao_new_hotel tnh inner join hotels h on tnh.qmg_hotelid=h.hotelid where tnh.hid ='" + hid + "' and h.hotel_parentid <>0)) and hid<>'" + hid + "'";
            DataTable db = SqlHelper.ExcuteDataTable(sql, null, SqlHelper.conStrRead);
            if (db == null || db.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                List<string> lstHid = new List<string>();
                foreach (DataRow item in db.Rows)
                {
                    string hidstr = item["hid"].ToString();
                    if (IsHotelExist(hidstr))
                    {
                        lstHid.Add(hidstr);
                    }
                }
                if (lstHid.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        //判断酒店是否存在，清除脏数据
        private static bool IsHotelExist(string hid)
        {
            IHotelData HotelData = DataFactory.CreateHotelData();
            XhotelGetRequest req = new XhotelGetRequest();
            req.Hid = long.Parse(hid);
            var res = TopConfig.Execute(req);
            if (res.IsError && res.SubErrCode == "isv.invalid-parameter:HOTEL_NOT_EXIST")
            {
                HotelData.CleanOffLineData(hid);
                log.Info("清理酒店，hid:" + hid);
                return false;
            }
            return true;
        }

        public static List<Tuple<long, string>> GetHotelIdList(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string content = sr.ReadToEnd();
            string[] hotelIdStr = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Tuple<long, string>> hotelinfo = null;
            hotelinfo = Array.ConvertAll<string, Tuple<long, string>>(hotelIdStr, t => new Tuple<long, string>(long.Parse(t.Split(',')[0].Trim()), t.Split(',')[1].Trim())).ToList();
            return hotelinfo;
        }
    }
}
