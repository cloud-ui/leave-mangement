using LeaveMangement_Application.Common;
using LeaveMangement_Application.User;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Entity.Models;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Controllers.Web
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class UserController : Controller
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private readonly IUserAppService _userAppService;
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public FTPUtil _fTPUtil = new FTPUtil();
        public UserController(IUserAppService userAppService,IConfiguration configuration, ICommonAppService commonAppService)
        {
            _userAppService = userAppService;
            _configuration = configuration;
            _commonAppService = commonAppService;
        }
        /// <summary>
        /// 后台管理登录（公司管理层员工）
        /// </summary>
        /// <param name="user.account">账号</param>
        /// <param name="user.password"></param>
        [HttpPost]
        public object Login([FromBody]UserDto user)
        {
            Worker userResult = _userAppService.Login(user.Account, user.Password);
            var result = new object();
            if(userResult != null)
            {
                //set序列化,加入值
                //HttpContext.Session.SetString("currentUser", JsonConvert.SerializeObject(userResult));
                JWTUtil _jwtUtil = new JWTUtil();
                var token = _jwtUtil.GetJwt(userResult.Account,_configuration);
                result = new
                {
                    isSuccess = true,
                    message = "登录成功！",
                    user = userResult,
                    menu = _userAppService.GetMenu(userResult.PositionId),
                    token
                };
            }
            else
                result = new
                {
                    isSuccess = true,
                    message = "登录失败！"
                };
            return result;
        }
        /// <summary>
        /// 单个添加员工
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddSingleWorker([FromBody]SingleWorkerDto singleWorkerDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            singleWorkerDto.CompanyId = _commonAppService.GetUserCompId(account);
            return _userAppService.AddSingleWorker(singleWorkerDto);
        }
        [HttpGet]
        [Authorize]
        public FileStream DownloadFile()
        {
            if(_fTPUtil.FTPIsConnected("123.207.242.177", "test", "zhang1997"))
            {
                FileStream fileStream = _fTPUtil.FTPIsdownload("123.207.242.177", "test", "zhang1997", "/home/test/cloudy", "test.txt");
                return fileStream;
            }
            return null;
        }
        /// <summary>
        /// 批量添加员工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> AddSingleWorker()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return true;
        }
        /// <summary>
        /// 获取员工详细信息
        /// </summary>
        /// <param name="userId">员工ID</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object GetWorkerById(int userId)
        {
            return _userAppService.GetWorkerById(userId);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object ModifyPassword([FromBody]ModifyPasswordDto modifyPasswordDto)
        {
            return _userAppService.ModifyPassword(modifyPasswordDto);
        }
        /// <summary>
        /// 完善用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object EditUserMessage([FromBody] EditUserMessageDto editUserMessageDto)
        {
            return _userAppService.EditUserMessage(editUserMessageDto);
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
            return true;
        }
    }
}