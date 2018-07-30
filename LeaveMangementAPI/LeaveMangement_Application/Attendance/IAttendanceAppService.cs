using LeaveMangement_Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Attendance
{
    public interface IAttendanceAppService
    {
        Result Clock(string address, string account, int compId);
    }
}
