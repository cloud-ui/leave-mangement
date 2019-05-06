using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.User;

namespace LeaveMangement_Core.Attendance
{
    public class AttendanceManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        //根据当前登录用户的地理位置打卡
        public Result Clock(ClockDto address, string account, int compId)
        {
            //找到公司员工
            //根据地址判断员工是否在公司
            //获取签到的日期
            //获取签到的时间
            //
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
            Company company = _ctx.Company.SingleOrDefault(c => c.Id == compId);
            Result result = new Result();
            if (CheckLocation(company,address))
            {
                DateTime dt = DateTime.Now;
                string clockDay = dt.ToString("yyyy-MM-dd");
                result = ClockIn(worker.Id, dt, clockDay);
            }
            else
            {
                result.IsSuccess = false;
                result.Message = "您当前定位不在公司附近！";
            }
            return result;
        }
        private bool CheckLocation(Company company,ClockDto clockDto)
        {
            double lng= Math.Abs(company.Lng - clockDto.Lng);
            double lat = Math.Abs(company.Lat - clockDto.Lat);
            if (lng <= 1 && lat <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //上班打卡
        private Result ClockIn(int workerId, DateTime dateTime,string clockDay)
        {
            ////判断是否迟到 签到时间大于9.30 迟到,迟到超过30分钟按一小时计算
            //int minute = dateTime.Minute, hour = dateTime.Hour;
            //bool isLateHour = hour > 9 ? true : false;
            //bool isLate = isLateHour && minute > 30 ? true : false;
            //int lateHour = isLate && minute - 30 > 0 ? hour - 8 : hour - 9;
            Result result = new Result();
            try
            {
                Clock clock = _ctx.Clock.SingleOrDefault(c => c.WorkId == workerId && c.ClockDay.Equals(clockDay));
                if (clock != null)
                {
                    clock.EndTime = dateTime.ToFileTime();
                    clock.WorkHour = GetWorkHour(clock.SrartTime, dateTime.ToFileTime());
                    clock.IsFull = true;
                }
                else
                {
                    Clock newClock = new Clock()
                    {
                        WorkId = workerId,
                        ClockDay = clockDay,
                        SrartTime = dateTime.ToFileTime(),
                        IsFull = false,
                    };
                    _ctx.Clock.Add(newClock);
                }
                _ctx.SaveChanges();
                result.IsSuccess = true;
                result.Message = "打卡成功！";
            }
            catch(Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }
        public object GetAttendanceData(string account, int companyId)
        {
            var result = new object();
            List<Deparment> deparments = _ctx.Deparment.Where(d => d.CompanyId == companyId).ToList();
            result = new
            {
                xData = GetDepName(deparments),
                totalData = new
                {
                    name="总人数",
                    data=GetDepWorkerCount(deparments)
                },
                clockData = new
                {
                    name="打卡人数",
                    data = GetDepClockCount(deparments)
                }
            };
            return result;
        }
        private List<int> GetDepClockCount(List<Deparment> deparments)
        {
            DateTime now = DateTime.Now;
            string day = now.ToString("yyyy-MM-dd");
            List<int> counts = new List<int>();
            foreach (Deparment deparment in deparments)
            {
                var clocks = (from clock in _ctx.Clock
                              join worker in _ctx.Worker on clock.WorkId equals worker.Id
                              where worker.DepartmentId == deparment.Id && clock.ClockDay.Equals(day)
                              select clock).ToList();
                counts.Add(clocks.Count());
            }
            return counts;
        }
        private List<int> GetDepWorkerCount(List<Deparment> deparments)
        {
            List<int> counts = new List<int>();
            foreach (Deparment deparment in deparments)
            {
                counts.Add(deparment.WorkerCount);
            }
            return counts;
        }
        private List<string> GetDepName(List<Deparment> deparments)
        {
            List<string> names = new List<string>();
            foreach(Deparment deparment in deparments)
            {
                names.Add(deparment.Name);
            }
            return names;
        }
        private double GetWorkHour(long start,long end)
        {
            DateTime startTime = DateTime.FromFileTime(start);
            DateTime endTime = DateTime.FromFileTime(end);
            TimeSpan timeSpan = endTime - startTime;
            return timeSpan.TotalMinutes / 60;
        }
    }
}
