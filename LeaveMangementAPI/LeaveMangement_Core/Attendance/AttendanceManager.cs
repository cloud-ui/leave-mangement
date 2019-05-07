﻿using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Core.Common;

namespace LeaveMangement_Core.Attendance
{
    public class AttendanceManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private CommonManager _commonManager = new CommonManager();
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
            if (CheckLocation(company, address))
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
        private bool CheckLocation(Company company, ClockDto clockDto)
        {
            double lng = Math.Abs(company.Lng - clockDto.Lng);
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
        private Result ClockIn(int workerId, DateTime dateTime, string clockDay)
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
            catch (Exception e)
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
                    name = "总人数",
                    data = GetDepWorkerCount(deparments)
                },
                clockData = new
                {
                    name = "打卡人数",
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
            foreach (Deparment deparment in deparments)
            {
                names.Add(deparment.Name);
            }
            return names;
        }
        private double GetWorkHour(long start, long end)
        {
            DateTime startTime = DateTime.FromFileTime(start);
            DateTime endTime = DateTime.FromFileTime(end);
            TimeSpan timeSpan = endTime - startTime;
            return timeSpan.TotalMinutes / 60;
        }

        /// <summary>
        /// 获取员工的出勤情况
        /// 月份-打卡天数-请假次数
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public object AttendanceByWorker(string account)
        {
            List<object> resultList = new List<object>();
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
            var clockList = (from clock in _ctx.Clock
                             let day = clock.ClockDay
                             let month = day.Substring(0, day.LastIndexOf('-'))
                             group clock by month into temp
                             select temp).ToList();
            foreach (var item in clockList)
            {
                int count = item.Count();
                var temp = new
                {
                    month = item.Key,
                    clockCount = count,
                    leaveCount = _commonManager.GetLeaveCount(account, worker.CompanyId)
                };
                resultList.Add(temp);
            }
            var result = new
            {
                count = clockList.Count,
                data = resultList
            };

            return result;
        }

        /// <summary>
        /// 获取登录用户选择月份的出勤情况
        /// </summary>
        /// <param name="account"></param>
        /// <param name="attendanceDto"></param>
        /// <returns></returns>
        public object AttendanceByMonth(string account, AttendanceDto attendanceDto)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account == account);
            //获取到打卡的日期数组
            var clockDays = (from clock in _ctx.Clock
                             let day = clock.ClockDay
                             let month = day.Substring(0, day.LastIndexOf('-'))
                             where month == attendanceDto.Month && clock.WorkId == worker.Id
                             select clock.ClockDay).ToArray();
            //获取请假的日期数组
            var applys = (from apply in _ctx.Apply
                          //let date = DateTime.FromFileTime(apply.StartTime).ToString("yyyy-MM-dd")
                          //let month = date.Substring(0, date.LastIndexOf('-'))
                          where apply.WorkerId == worker.Id 
                          select new
                          {
                              apply.StartTime,
                              apply.EndTime
                          }).ToList();
            foreach(var item in applys)
            {
                Console.WriteLine(DateTime.FromFileTime(item.StartTime).ToString("yyyy-MM-dd"));
            }

            return true;
        }

        private object GetWorkDay(long startTime,long endTime)
        {
            DateTime start = DateTime.FromFileTime(startTime);
            DateTime end = DateTime.FromFileTime(endTime);
            TimeSpan span = end - start;
            //int totleDay=span.Days;
            //DateTime spanNu = DateTime.Now.Subtract(span);
            int AllDays = Convert.ToInt32(span.TotalDays) + 1;//差距的所有天数
            int totleWeek = AllDays / 7;//差别多少周
            int yuDay = AllDays % 7; //除了整个星期的天数
            int lastDay = 0;
            if (yuDay == 0) //正好整个周
            {
                lastDay = AllDays - (totleWeek * 2);
            }
            else
            {
                int weekDay = 0;
                int endWeekDay = 0; //多余的天数有几天是周六或者周日
                switch (start.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        weekDay = 1;
                        break;
                    case DayOfWeek.Tuesday:
                        weekDay = 2;
                        break;
                    case DayOfWeek.Wednesday:
                        weekDay = 3;
                        break;
                    case DayOfWeek.Thursday:
                        weekDay = 4;
                        break;
                    case DayOfWeek.Friday:
                        weekDay = 5;
                        break;
                    case DayOfWeek.Saturday:
                        weekDay = 6;
                        break;
                    case DayOfWeek.Sunday:
                        weekDay = 7;
                        break;
                }
                if ((weekDay == 6 && yuDay >= 2) || (weekDay == 7 && yuDay >= 1) || (weekDay == 5 && yuDay >= 3) || (weekDay == 4 && yuDay >= 4) || (weekDay == 3 && yuDay >= 5) || (weekDay == 2 && yuDay >= 6) || (weekDay == 1 && yuDay >= 7))
                {
                    endWeekDay = 2;
                }
                if ((weekDay == 6 && yuDay < 1) || (weekDay == 7 && yuDay < 5) || (weekDay == 5 && yuDay < 2) || (weekDay == 4 && yuDay < 3) || (weekDay == 3 && yuDay < 4) || (weekDay == 2 && yuDay < 5) || (weekDay == 1 && yuDay < 6))
                {
                    endWeekDay = 1;
                }
                lastDay = AllDays - (totleWeek * 2) - endWeekDay;
            }
            return true;
        }
    }
}
