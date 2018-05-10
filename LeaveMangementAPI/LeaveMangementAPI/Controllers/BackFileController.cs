using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveMangement_Application.DangAn;
using LeaveMangement_Entity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}