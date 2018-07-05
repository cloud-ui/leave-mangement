using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.Common;
using LeaveMangement_Application.Permission;
using LeaveMangement_Entity.Dtos.Permission;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LeaveMangementAPI.Controllers.Web
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class PermissionController : Controller
    {
        private readonly ICommonAppService _commonAppService;
        private readonly IPermissionAppService _permissionAppServer;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public PermissionController(IConfiguration configuration, ICommonAppService commonAppService,IPermissionAppService permissionAppService)
        {
            _configuration = configuration;
            _commonAppService = commonAppService;
            _permissionAppServer = permissionAppService;
        }
        /// <summary>
        /// 获取目录树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public object GetMenuTree()
        {
            return _permissionAppServer.GetMenuTree();
        }
        /// <summary>
        /// 获取目录根据职位
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object GetMenuTreeByPostion(int positionId)
        {
            return _permissionAppServer.GetMenuTreeByPostion(positionId);
        }
        /// <summary>
        /// 提交权限配置
        /// </summary>
        /// <param name="selectMenuDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object SaveSelectMenu([FromBody]SelectMenuDto selectMenuDto)
        {
            return _permissionAppServer.SaveSelectMenu(selectMenuDto);
        }
    }
}