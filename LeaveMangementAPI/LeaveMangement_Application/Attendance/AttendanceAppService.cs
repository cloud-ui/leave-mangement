using LeaveMangement_Core.Attendance;
using LeaveMangement_Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Attendance
{
    public class AttendanceAppService : IAttendanceAppService
    {
        private readonly AttendanceManager _attendanceManager = new AttendanceManager();
        public Result Clock(string address, string account, int compId)
        {
            return _attendanceManager.Clock(address, account, compId);
        }
    }
}
