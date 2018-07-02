using LeaveMangement_Application.Common;
using LeaveMangement_Application.User;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Entity.Model;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        [HttpGet]
        public bool CreateUser()
        {
            AdminUser user = new AdminUser()
            {
                Account = "123456789",
                Password = "123456789",
                Name = "joy",
                Status = "systemAdmin",
            };
            _ctx.AdminUser.Add(user);
            _ctx.SaveChanges();
            return true;
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

    }
}