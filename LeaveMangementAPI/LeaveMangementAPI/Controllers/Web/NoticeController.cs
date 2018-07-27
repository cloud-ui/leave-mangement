using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Common;
using LeaveMangement_Application.Notice;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Notices;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeaveMangementAPI.Controllers.Web
{
    /// <summary>
    /// 公告管理
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class NoticeController : Controller
    {
        private readonly ICommonAppService _commonAppService;
        private readonly INoticeAppService _noticeAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public NoticeController(INoticeAppService noticeAppService, IConfiguration configuration, ICommonAppService commonAppService)
        {
            _commonAppService = commonAppService;
            _noticeAppService = noticeAppService;
            _configuration = configuration;
        }
        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> NoticeList([FromBody]QueryList queryList)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _noticeAppService.NoticeList(queryList, account);
        }
        /// <summary>
        /// 添加公告
        /// </summary>
        /// <param name="addNotice"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<Result> AddNotice([FromBody]AddNotice addNotice)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return _noticeAppService.AddNotice(addNotice, account);
        }
        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public Result DeleteNotice(int id)
        {
            return _noticeAppService.DeleteNotice(id);
        }
    }
}