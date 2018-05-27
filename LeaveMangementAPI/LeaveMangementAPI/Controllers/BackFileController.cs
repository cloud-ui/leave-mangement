using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LeaveMangement_Application.DangAn;
using LeaveMangement_Entity.Models;
using LeaveMangementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LeaveMangementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class BackFileController : Controller
    {
        private readonly IDangAnAppService _dangAnAppService;
        public BackFileController(IDangAnAppService dangAnAppService)
        {
            _dangAnAppService = dangAnAppService;
        }
        [HttpGet]
        public List<Company> GetCompanyList()
        {
            return _dangAnAppService.GetCompanyList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        [HttpGet]
        public Company GetCompanyById(int compId)
        {
            return _dangAnAppService.GetCompanyById(compId);
        }
        [HttpPost]
        public object AddCompany(Company company)
        {
            return _dangAnAppService.AddCompany(company);
        }
        [HttpPost]
        public object SendAuthCode(string phone)
        {
            return _dangAnAppService.SendMessage(phone);
        }
        //[HttpPut]
        //public object EditCompany(Company company)
        //{
        //    return _dangAnAppService.EditCompany(company);
        //}
        [HttpDelete]
        public object DeleteCompany(int compId)
        {
            return _dangAnAppService.DeleteCompany(compId);
        }
        /// <summary>
        /// 获取当前登录用户所在公司的部门列表
        /// </summary>
        /// <param name="compId"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetDeparmentList(int compId)
        {
            return _dangAnAppService.GetDeparmentList(compId);
        }

        /// <summary>
        /// 获取部门的员工列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public object GetWorkListByDepId(QueryModel query)
        {
            return 0;
        }
        /// <summary>
        /// 获取当前用户公司的员工列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public object GetWorkList()
        {
            //var user = HttpContext.Session.GetString("currentUser");
            //var currentUser = JsonConvert.DeserializeObject<Worker>(user);
            var currentToken = Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();
            return Request.Headers["Authorization"];
        }
    }
}