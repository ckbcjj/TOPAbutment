using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;
using System.Threading;
using Taobao.Top2.Application;
using Taobao.Top2.Application.Implement;
using Common.Tool;

namespace TOPAbutment
{
    public class ScheduleManager
    {
        /// <summary>
        /// 时间间隔执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="seconds">时间间隔(单位：秒)</param>
        public static void ExecuteInterval<T>(int seconds) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();
            IJobDetail job = JobBuilder.Create<T>().Build();
            job.JobDataMap.Add("Interval", seconds);
            ITrigger trigger = TriggerBuilder.Create()
               .StartNow()
               .WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever())
               .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }
        /// <summary>
        /// 指定时间执行任务
        /// </summary>
        /// <typeparam name="T">任务类，必须实现IJob接口</typeparam>
        /// <param name="cronExpression">cron表达式，即指定时间点的表达式</param>
        public static void ExecuteByCron<T>(string cronExpression) where T : IJob
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler();

            IJobDetail job = JobBuilder.Create<T>().Build();
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                .WithCronSchedule(cronExpression)
                .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }
    }

    //增量
    public class RatesIncrementJob : IJob
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IProductUpload upload = new ProductUpload();
        int advanceTime = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RatesIncrementAdvanceInterval"]);
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                DateTime endtime = DateTime.Now;
                int Minutes = Convert.ToInt32(context.JobDetail.JobDataMap["Interval"]) / 60;
                DateTime begintime = endtime.AddMinutes(-Minutes).AddSeconds(-advanceTime);
                log.Info("增量更新开始");
                upload.UpdateProductsIncr(begintime, endtime);
                log.Info(string.Format("增量更新结束"));
            }
            catch (Exception err)
            {
                log.Error("发生未知异常，请处理。" + err.StackTrace);
            }
        }
    }

    public class HotelRoomSynJob : IJob
    {
        private static Log4Helper log = Log4Factory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IHotelUpload hu = new HotelUpload();
        IRoomUpload ru = new RoomUpload();
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                hu.SynHotelsStatus();//定时同步淘宝酒店和供应商酒店状态
                ru.SynRoomTypeStatus();//定时同步淘宝房型和供应商房型状态
            }
            catch (Exception err)
            {
                log.Error("发生未知异常，请处理。" + err.StackTrace);
            }
        }
    }
}
