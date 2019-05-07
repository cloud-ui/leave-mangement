using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Attendance
{
    public interface IAttendanceAppService
    {
        Result Clock(ClockDto address, string account, int compId);
        object GetAttendanceData(string account, int companyId);

        object AttendanceByWorker(string account);

        object AttendanceByMonth(string account, AttendanceDto attendanceDto);
    }
}
