using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Approval;
using LeaveMangement_Application.Common;
using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeaveMangementAPI.Controllers
{
    /// <summary>
    /// 审批
    /// </summary>
    [Produces("application/json")]
    [Route("api/BackApproval")]
    public class BackApprovalController : Controller
    {
        /// <summary>
        /// 审批
        /// </summary>
        /// <returns></returns>
        private readonly IApprovalAppService _approvalAppService;
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public BackApprovalController(IApprovalAppService approvalAppService,IConfiguration configuration,ICommonAppService commonAppService)
        {
            _approvalAppService = approvalAppService;
            _commonAppService = commonAppService;
            _configuration = configuration;
        }
        /// <summary>
        /// 提交请假申请
        /// </summary>
        /// <param name="addApplicationDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddApplication([FromBody] AddApplicationDto addApplicationDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            addApplicationDto.CompId = _commonAppService.GetUserCompId(account);
            addApplicationDto.DeparmentId = _commonAppService.GetUserDepId(account);
            return _approvalAppService.AddApplication(addApplicationDto);
        }
        /// <summary>
        /// 获取已提交的请假列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetApplicationList()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.GetApplicationList(account);
        }
        /// <summary>
        /// 获取一条请假申请的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public object GetApplicationById(int id)
        {
            return _approvalAppService.GetApplicationById(id);
        }
        /// <summary>
        /// 获取登录用户未提交的申请
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetUnApplicationList()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _approvalAppService.GetUnApplicationList(account);
        }
        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object SubmitApplication([FromBody]int id)
        {
            return _approvalAppService.SubmitApplication(id);
        }
        /// <summary>
        /// 编辑未提交的申请
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object EditApplication([FromBody]EditApplicationDto editApplicationDto)
        {

            return _approvalAppService.EditApplication(editApplicationDto);
        }
    }
}