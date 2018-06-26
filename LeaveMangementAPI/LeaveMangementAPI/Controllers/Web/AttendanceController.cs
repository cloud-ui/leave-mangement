using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Common;
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
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public AttendanceController(ICommonAppService commonAppService,IConfiguration configuration)
        {
            _commonAppService = commonAppService;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Clock([FromBody]string address)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int companyId = _commonAppService.GetUserCompId(account);
            return true;
        }
    }
}