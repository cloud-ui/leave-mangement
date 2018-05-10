using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.User;
using LeaveMangement_Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMangementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BackUserController : Controller
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private readonly IUserAppService _userAppService;
        public BackUserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        /// <summary>
        /// 后台管理登录（公司管理层员工）
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        [HttpPost]
        public object Login(string account,string password)
        {
            return _userAppService.Login(account, password);
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