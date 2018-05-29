using LeaveMangement_Application.User;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Models;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LeaveMangementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class BackUserController : Controller
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private readonly IUserAppService _userAppService;
        public IConfiguration _configuration;        
        public BackUserController(IUserAppService userAppService,IConfiguration configuration)
        {
            _userAppService = userAppService;
            _configuration = configuration;
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

        
    }
}