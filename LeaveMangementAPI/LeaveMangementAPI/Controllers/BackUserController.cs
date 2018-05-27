using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LeaveMangement_Application.User;
using LeaveMangement_Entity.Models;
using LeaveMangementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        public object Login([FromBody]User user)
        {
            Worker userResult = _userAppService.Login(user.account, user.password);
            var result = new object();
            if(userResult != null)
            {
                //set序列化,加入值
                HttpContext.Session.SetString("currentUser", JsonConvert.SerializeObject(userResult));
                result = new
                {
                    isSuccess = true,
                    message = "登录成功！",
                    user = userResult,
                    token = Token(userResult.Account).Result.Value

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

        #region jwt
        [HttpPost]

        public async Task<JsonResult> Token(string account)
        {
            var context = HttpContext;
            var userClaims = await GetTokenClaims(account);
            if (userClaims == null)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(JsonConvert.SerializeObject("账号或密码错误!"));
                return Json("");
            }
            var audienceConfig = _configuration.GetSection("TokenAuthentication:Audience").Value;
            var symmetricKeyAsBase64 = _configuration.GetSection("TokenAuthentication:SecretKey").Value;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var jwtToken = new JwtSecurityToken(
                issuer: audienceConfig,
                audience: audienceConfig,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: new SigningCredentials(
                    signingKey,
                    SecurityAlgorithms.HmacSha256)
               );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            var token_bearer = "Bearer " + token;
            var response = new
            {
                IsSuccess = true,
                Data = new
                {
                    token = token_bearer,
                    expiration = jwtToken.ValidTo
                }
            };
            return Json(response);
        }

        [HttpGet]
        private async Task<IEnumerable<Claim>> GetTokenClaims(string account)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account)
            };
        }
        #endregion
    }
}