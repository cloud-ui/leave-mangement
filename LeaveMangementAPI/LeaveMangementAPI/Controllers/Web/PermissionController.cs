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
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class PermissionController : Controller
    {
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public PermissionController(IConfiguration configuration, ICommonAppService commonAppService)
        {
            _configuration = configuration;
            _commonAppService = commonAppService;
        }
        [HttpGet]
        [Authorize]
        public object GetMenuTree()
        {
            return true;
        }
    }
}