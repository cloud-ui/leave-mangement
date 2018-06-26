using LeaveMangement_Entity.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LeaveMangement_Core.Attendance
{
    public class AttendanceManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        //根据当前登录用户的地理位置打卡
        public object Clock(string address, string account, int compId)
        {
            //找到公司员工
            //根据地址判断员工是否在公司
            //获取签到的日期
            //获取签到的时间
            //
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)); 
            Company company = _ctx.Company.SingleOrDefault(c => c.Id == compId && c.Address.Contains(address));
            var result = new object();
            if (company != null)
            {
                DateTime dt = DateTime.Now;
                string clockDay = dt.ToString("yyyy-MM-dd");
                string clockTime = dt.ToLongTimeString().ToString();
            }
            else
                result = new
                {
                    isSuccess = false,
                    message = "您当前定位不在公司附近！"
                };
            return result;
        }
        //上班打卡
        public void ClockIn(int workerId, DateTime dateTime)
        {
            //判断是否迟到 签到时间大于9.30 迟到,迟到超过30分钟按一小时计算
            int minute = dateTime.Minute, hour = dateTime.Hour;
            bool isLateHour = hour > 9 ? true : false;
            bool isLate = isLateHour && minute > 30 ? true : false;
            int lateHour = isLate && minute - 30 > 0 ? hour - 8 : hour - 9;
            //Clock newClock = new Clock()
            //{
            //    WorkId = workerId,
            //    ClockDay = clockDay,
            //    SrartTime = clockTime,
            //    IsFull = false,
            //};
            //_ctx.Clock.Add(newClock);
            //_ctx.SaveChanges();
        }
        //下班打卡
        public void ClockOut(int workerId,DateTime dateTime)
        {
            //根据当天打卡的时间和用户id找到打卡记录
            Clock clock = _ctx.Clock.SingleOrDefault(c => c.WorkId == workerId && c.ClockDay.Equals(dateTime.ToString("yyyy-MM-dd")));
            //判断是否早退


        }
    }
}
