using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LeaveMangement_Application.Common;
using LeaveMangement_Application.DangAn;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Model;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace LeaveMangementAPI.Controllers.Web
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class FileController : Controller
    {
        private readonly IDangAnAppService _dangAnAppService;
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public FileController(IDangAnAppService dangAnAppService,IConfiguration configuration,ICommonAppService commonAppService)
        {
            _configuration = configuration;
            _dangAnAppService = dangAnAppService;
            _commonAppService = commonAppService;
        }        
        /// <summary>
        /// 获取登录用户所在的公司信息
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetCompanyInfo()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetCompanyById(compId);
        }
        /// <summary>
        /// 注册新公司
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public object AddCompany(Company company)
        {
            return _dangAnAppService.AddCompany(company);
        }
        /// <summary>
        /// 添加新公司时发送验证码(邮箱)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost]
        public object SendAuthCode(string email)
        {
            return _dangAnAppService.SendMessage(email);
        }
        /// <summary>
        /// 单个添加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddSingleDpearment([FromBody]AddSingleDeparmentDto addSingleDeparmentDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            addSingleDeparmentDto.CompId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.AddSingleDpearment(addSingleDeparmentDto);
        }
        /// <summary>
        /// 批量添加部门
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddMulitDeparment()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            return true;
        }
        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="addSingleDeparmentDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object EditDeparment([FromBody]AddSingleDeparmentDto addSingleDeparmentDto)
        {
            return _dangAnAppService.EditDeparment(addSingleDeparmentDto);
        }
        /// <summary>
        /// 获取当前登录用户所在公司的部门列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetDeparmentList([FromBody]DepartmentDto query)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            query.CompId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetDeparmentList(query);
        }

        /// <summary>
        /// 添加部门获取到员工
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetWorkerList()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetWorkerList(compId);
        }
        /// <summary>
        /// 获取当前用户公司的员工列表/获取部门的员工列表(传部门ID)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> GetWorkList([FromBody]WorkDto query)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            query.CompId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetWorkList(query);
        }
        /// <summary>
        /// 获取到登录用户所在公司的职位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetPositionList()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetPositionListByCompId(compId);
        }
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public object DeletePositionById(int id)
        {
            return _dangAnAppService.DeletePosition(id);
        }
        /// <summary>
        /// 添加职位
        /// </summary>
        /// <param name="addStateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddPosition([FromBody] AddStateDto addStateDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            addStateDto.CompId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.AddPosition(addStateDto);
        }
        /// <summary>
        /// 编辑职位
        /// </summary>
        /// <param name="addStateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object EditPosition([FromBody] AddStateDto addStateDto)
        {
            return _dangAnAppService.EditPosition(addStateDto);
        }
        /// <summary>
        /// 获取到登录用户所在公司的员工职位状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetStateList()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetStateListByCompId(compId);
        }
        ///// <summary>
        ///// 删除员工状态
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpDelete]
        //[Authorize]
        //public object DeleteStateById(int id)
        //{
        //    return _dangAnAppService.DeleteState(id);
        //}
        /// <summary>
        /// 添加员工状态
        /// </summary>
        /// <param name="addStateDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<object> AddState([FromBody] AddStateDto addStateDto)
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            addStateDto.CompId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.AddState(addStateDto);
        }
        /// <summary>
        /// 编辑员工状态
        /// </summary>
        /// <param name="addStateDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object EditState([FromBody] AddStateDto addStateDto)
        {
            return _dangAnAppService.EditState(addStateDto);
        }

    }
}