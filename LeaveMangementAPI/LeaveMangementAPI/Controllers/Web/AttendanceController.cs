using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Attendance;
using LeaveMangement_Application.Common;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeaveMangementAPI.Controllers.Web
{
    /// <summary>
    /// 考勤
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class AttendanceController : Controller
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IAttendanceAppService _attendanceAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public AttendanceController(IAttendanceAppService attendanceAppService,ICommonAppService commonAppService,IConfiguration configuration)
        {
            _commonAppService = commonAppService;
            _configuration = configuration;
            _attendanceAppService = attendanceAppService;
        }
        /// <summary>
        /// 打卡签到
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Result> Clock([FromBody]ClockDto address)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int companyId = _commonAppService.GetUserCompId(account);
            return _attendanceAppService.Clock(address, account, companyId);
        }
        /// <summary>
        /// 首页获取图表的出勤统计数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetAttendanceData()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int companyId = _commonAppService.GetUserCompId(account);
            return _attendanceAppService.GetAttendanceData(account, companyId);
        }

        /// <summary>
        /// 获取登录用户的出勤情况
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> AttendanceByWorker()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            var result = _attendanceAppService.AttendanceByWorker(account);
            return result;
        }
    }
}