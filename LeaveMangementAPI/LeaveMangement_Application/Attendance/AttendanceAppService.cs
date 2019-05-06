using LeaveMangement_Core.Attendance;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Attendance
{
    public class AttendanceAppService : IAttendanceAppService
    {
        private readonly AttendanceManager _attendanceManager = new AttendanceManager();

        public object AttendanceByWorker(string account)
        {
            return _attendanceManager.AttendanceByWorker(account);
        }

        public Result Clock(ClockDto address, string account, int compId)
        {
            return _attendanceManager.Clock(address, account, compId);
        }
        public object GetAttendanceData(string account, int companyId)
        {
            return _attendanceManager.GetAttendanceData(account, companyId);
        }
    }
}
