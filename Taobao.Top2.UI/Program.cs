using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Tool;
using Taobao.Top2.Application;
using Taobao.Top2.Application.Implement;
using System.Data.SqlClient;
using Taobao.Top2.Entity.OrderEntity;
using System.IO;
using System.Text;
using System.Diagnostics;
using Top.Api.Request;
using Top.Api;
using Top.Api.Response;

namespace TOPAbutment
{
    public class Program
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static void Main(string[] args)
        {
            #region Go Go Go
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Title = System.Configuration.ConfigurationManager.AppSettings["appName"];
            Console.Clear();
                //---------------------------------------我是可爱的分割线-------------------------------------------//
               //                                                                                                  //
              //        0:淘宝发货；    -1:淘宝落单；     -2:新增酒店；          -3:全量更新；                    //
             //       -4:上下线酒店；  -5:删除酒店；     -6:酒店状态同步        其它:增量+状态同步               //
            //                                                                                                  //
           //-----------------------------------------我是可爱的分割线-----------------------------------------//

            int m = -3;

            //酒店房型状态同步
            if (m == -6)
            {
                IHotelUpload hu = new HotelUpload();
                IRoomUpload ru = new RoomUpload();
                hu.SynHotelsStatus();//定时同步淘宝酒店和供应商酒店状态
                ru.SynRoomTypeStatus();//定时同步淘宝房型和供应商房型状态
            }

            //删除酒店
           else if (m == -5)
            {
                log.Info("删除酒店开始");
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                long[] hidList = GetHidList(filePath);
                if (hidList == null || hidList.Count() == 0)
                {
                    log.Info("无需要删除的酒店");
                }
                else
                {
                    IProductUpload upload = new ProductUpload();
                    upload.DeleteHotel(hidList.ToList());
                }
                log.Info("删除酒店结束");
            }

            //上下线酒店
            else if (m == -4)
            {
                bool isup = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["isup"]) == 1 ? true : false;
                string msg = isup ? "上线酒店" : "下线酒店";
                log.Info(string.Format("{0}开始", msg));
                //需要上下线的酒店
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                int[] hotelIds = GetHotelIdList(filePath);
                if (hotelIds == null || hotelIds.Count() == 0)
                {
                    log.Info("无需要上下线的酒店");
                }
                else
                {
                    IProductUpload upload = new ProductUpload();
                    upload.ProductStatusUpateByHotelid(hotelIds.ToList(), isup);
                }
                log.Info(string.Format("{0}结束", msg));
            }

            //全量更新价格
            else if (m == -3)
            {
                log.Info("全量更新开始");
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                int[] hotelIds = GetHotelIdList(filePath);//只对特定一些酒店全量更新
                IProductUpload upload = new ProductUpload();
                 upload.PriceUpdate(hotelIds);
                log.Info("全量更新结束");
            }

            //上传酒店
            else if (m == -2)
            {
                log.Info("导入酒店开始");
                IHotelUpload hotelUpload = new HotelUpload();
                string filePath = AppDomain.CurrentDomain.BaseDirectory;
                int[] hotelIds = GetHotelIdList(filePath);
                if (hotelIds == null || hotelIds.Count() == 0)
                {
                    log.Info("无需要上线的酒店");
                }
                else
                {
                    log.Info("酒店数据上传中");
                    var taobaoHotelList = hotelUpload.UpLoad(hotelIds.ToList());
                    if (taobaoHotelList.Count != hotelIds.Count())
                    {
                        var faildlst = hotelIds.AsEnumerable().Where(t => !taobaoHotelList.Select(p => p.qmg_HotelId).Contains(t));//失败的
                        log.Info(string.Format("上线失败酒店{0}个:{1}", faildlst.Count(), string.Join(",", faildlst)));
                    }
                    log.Info("酒店数据上传完成");
                }
                log.Info("导入酒店结束");
            }

            //同步订单
            else if (m == -1)
            {
                IOrderUpload upload = new OrderUpload();
                while (true)
                {
                    log.Info("同步订单开始");
                    //获取淘宝订单信息
                    List<HotelOrderInfo> hotelOrders = upload.GetTaobaoOrders();
                    log.Info("数据规整后淘宝订单列表：" + string.Join(",", hotelOrders.Select(model => model.PartnerOrderId).ToList()));
                    //生成新订单
                    List<HotelOrderInfo> hotelNewOrders = upload.AddNewOrder(hotelOrders);
                    List<string> newHotelOrderIdList = hotelNewOrders.Select(model => model.OrderId.ToString()).ToList();
                    List<string> newSendOrderIdlist = hotelNewOrders.Select(model => model.PartnerOrderId).ToList();
                    log.Info("生成酒店订单列表：" + string.Join(",", newHotelOrderIdList));
                    log.Info("酒店订单对应淘宝订单列表：" + string.Join(",", newSendOrderIdlist));
                    List<HotelOrderInfo> hotelNewOrderRooms = upload.AddOrderRoom(hotelNewOrders);//更新青芒果库数据
                    log.Info("执行完成订单列表：" + string.Join(",", hotelNewOrderRooms.Select(model => model.OrderId.ToString()).ToList()));
                    log.Info("同步订单结束");
                    Thread.Sleep(1 * 60000);
                }
            }

            //发货
            else if (m == 0)
            {
                IOrderUpload upload = new OrderUpload();
                while (true)
                {
                    log.Info("发货开始");
                    //获取淘宝订单信息
                    List<HotelOrderInfo> hotelOrders = upload.GetTaobaoOrders();
                    upload.SetTaobaoOrderStart(hotelOrders);
                    log.Info("发货结束");
                    Thread.Sleep(1 * 60000);
                }
            }

            //增量更新价格
            else
            {
                ScheduleManager.ExecuteInterval<RatesIncrementJob>(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RatesIncrementInterval"]));
            }
            #endregion
        }

        #region  //测试
        /// <summary>
        /// 清除历史日志信息
        /// </summary>
        private static void DeleteLogMsg()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@date", DateTime.Now.AddDays(-5).Date.ToString("yyyy-MM-dd"))
            };
            SqlHelper.ExecuteNonQuery("delete from taobao_top2_log4msg where convert(varchar(10),dtdate,120) <=@date",
                para, SqlHelper.conStr);
        }

        /// <summary>
        /// 从文本文件读取酒店ID列表
        /// </summary>
        /// <returns></returns>
        private static int[] GetHotelIdList(string path)
        {
            StreamReader sr = new StreamReader(path + "HotelIdList.txt", Encoding.Default);
            string content = sr.ReadToEnd();
            string[] hotelIdStr = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            int[] hotelIdInt = null;
            if (hotelIdStr.Length == 1 && hotelIdStr[0] == "")
            {
                return hotelIdInt;
            }
            hotelIdInt = Array.ConvertAll<string, int>(hotelIdStr, s => int.Parse(s));
            return hotelIdInt;
        }

        private static long[] GetHidList(string path)
        {
            StreamReader sr = new StreamReader(path + "HotelIdList.txt", Encoding.Default);
            string content = sr.ReadToEnd();
            string[] hotelIdStr = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            long[] HidList = null;
            if (hotelIdStr.Length == 1 && hotelIdStr[0] == "")
            {
                return HidList;
            }
            HidList = Array.ConvertAll<string, long>(hotelIdStr, s => long.Parse(s));
            return HidList;
        }


        private static List<string> Test1(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string content = sr.ReadToEnd();
            string[] hotelIdStr = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<string> hotelIdInt = null;
            if (hotelIdStr.Length == 1 && hotelIdStr[0] == "")
            {
                return hotelIdInt.ToList();
            }
            hotelIdInt = Array.ConvertAll<string, string>(hotelIdStr, s => s).ToList();
            return hotelIdInt;
        }

        private static List<Tuple<string, string, string, string>> Test2(string path)
        {
            StreamReader sr = new StreamReader(path, Encoding.Default);
            string content = sr.ReadToEnd();
            string[] str = content.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            List<Tuple<string, string, string, string>> lst = null;
            if (str.Length == 1 && str[0] == "")
            {
                return lst;
            }
            lst = Array.ConvertAll<string, Tuple<string, string, string, string>>(str, s => new Tuple<string, string, string, string>(s.Split(',')[0], s.Split(',')[1], s.Split(',')[2], s.Split(',')[3])).ToList();
            return lst;
        }

        private static void test()
        {
            ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest?", "23757801", "37999e3f0dde002bc14849a222ff8c08");
            AreasGetRequest req = new AreasGetRequest();
            req.Fields = "id,type,name,parent_id,zip";
            AreasGetResponse rsp = client.Execute(req);
            Console.WriteLine(rsp.Body);
            Console.ReadKey();
        }

        private static void test2()
        {
           var  eprice_field = System.Math.Ceiling((Convert.ToDouble(136) - Convert.ToDouble(3)) * 100 / 97);;
            var com_field = eprice_field - System.Math.Floor((Convert.ToDouble(136) - Convert.ToDouble(3)));
            Console.WriteLine("价格："+eprice_field);
            Console.WriteLine("佣金：" + com_field);
            Console.ReadKey();
        }

        private static void Test3()
        {
            var 全部下线 = Test1(@"C:\Users\qmango\Desktop\已全部下线.txt");
            var 不转全额 = Test1(@"C:\Users\qmango\Desktop\不转全额.txt");
            var 删除 = new List<string>();
            var 要上线 = new List<string>();
            foreach (var item in 全部下线)
            {
                if (不转全额.Contains(item))
                {
                    删除.Add(item);
                }
                else
                {
                    要上线.Add(item);
                }
            }
            File.AppendAllText(@"C:\Users\qmango\Desktop\要上线.txt", string.Join("\r\n", 要上线));
            File.AppendAllText(@"C:\Users\qmango\Desktop\不转全额(要删除).txt", string.Join("\r\n", 删除));
        }
        #endregion
    }
}