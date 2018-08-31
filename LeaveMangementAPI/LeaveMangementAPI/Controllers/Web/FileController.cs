using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LeaveMangement_Application.Common;
using LeaveMangement_Application.DangAn;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Models;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace LeaveMangementAPI.Controllers.Web
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Consumes("application/json", "multipart/form-data")]
    [EnableCors("any")]
    public class FileController : Controller
    {
        private readonly IDangAnAppService _dangAnAppService;
        private readonly ICommonAppService _commonAppService;
        public IConfiguration _configuration;
        public JWTUtil _jwtUtil = new JWTUtil();
        public ImportExcelUtil _importExcelUtil = new ImportExcelUtil(); 
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
        public object AddCompany([FromBody]Company company)
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
        /// 编辑公司资料
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public object EditCompany([FromBody]EditCompanyDto editCompanyDto)
        {
            return _dangAnAppService.EditCompany(editCompanyDto);
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
        //[Authorize]
        public object AddMulitDeparment(IFormCollection files)
        {
            string[] colName = new string[] { "公司名称", "部门经理", "部门名称", "员工数量", "部门代码" };
            if (files != null && files.Files.Count > 0)
            {
                for (int i = 0; i < files.Files.Count; i++)
                {
                    var file = files.Files[i];
                    try
                    {
                        object path = _importExcelUtil.SaveExcel(file);
                        FileInfo fileInfo = new FileInfo((string)path);
                        using (FileStream fs = new FileStream(fileInfo.ToString(), FileMode.Create))
                        {
                            file.CopyTo(fs);
                            fs.Flush();
                        }
                        using (ExcelPackage package = new ExcelPackage(fileInfo))
                        {
                            StringBuilder sb = new StringBuilder();
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                            int rowCount = worksheet.Dimension.Rows;
                            int ColCount = worksheet.Dimension.Columns;
                            bool bHeaderRow = true;
                            if (_importExcelUtil.JudgeCol(worksheet,colName))
                            {
                                List<Deparment> deparments = new List<Deparment>();
                                for (int row = 2; row <= rowCount; row++)
                                {
                                    Deparment deparment = new Deparment();
                                    for (int col = 1; col <= ColCount; col++)
                                    {
                                        switch (col)
                                        {
                                            case 1: deparment.CompanyId = _commonAppService.GetCompId(worksheet.Cells[row, col].Value.ToString());break;
                                            case 2: deparment.ManagerId = _commonAppService.GetUserId(worksheet.Cells[row, col].Value.ToString()); break;
                                            case 3: deparment.Name = worksheet.Cells[row, col].Value.ToString();break;
                                            case 4: deparment.WorkerCount = Convert.ToInt32( worksheet.Cells[row, col].Value); break;
                                            case 5: deparment.Code = worksheet.Cells[row, col].Value.ToString(); break;
                                            default:break;
                                        };
                                    }
                                    deparments.Add(deparment);
                                    
                                }
                            }

                            //return Content(sb.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                    
                }
            }
            else
            {
                return false;

            }

            return false;

        
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
        /// 删除部门
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public object DeleteDeparment(int id)
        {
            return _dangAnAppService.DeleteDeparment(id);
        }
        /// <summary>
        /// 根据部门编号获取到部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public object GetDeparment(int id)
        {
            return _dangAnAppService.GetDeparmentById(id);
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
        /// 员工列表，显示部门选择器数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<object> GetDeparments()
        {
            var context = HttpContext;
            string account = await _jwtUtil.GetMessageByToken(context);
            int compId = _commonAppService.GetUserCompId(account);
            return _dangAnAppService.GetDeparments(compId);
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

        /// <summary>
        /// 人事调动
        /// </summary>
        /// <param name="transferWorkerDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public Result TransferWorker([FromBody]TransferWorkerDto transferWorkerDto)
        {
            return _dangAnAppService.TransferWorker(transferWorkerDto);
        }
        [HttpGet]
        //[Authorize]
        public IActionResult DownloadFile()
        {
            var FilePath = @"./files/deparment.xlsx";
            var stream = System.IO.File.OpenRead(FilePath);
            return File(stream, "application/vnd.android.package-archive", Path.GetFileName(FilePath));
        }

    }
}