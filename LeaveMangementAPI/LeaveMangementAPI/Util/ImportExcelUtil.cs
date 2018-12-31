using LeaveMangement_Application.Common;
using LeaveMangement_Core.User;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LeaveMangementAPI.Util
{
    public class ImportExcelUtil
    {
        public ImportExcelUtil() { }
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        private UserService _userService = new UserService();
        private KaoQinContext _ctx = new KaoQinContext();
        public object SaveExcel(IFormFile file)
        {
            string completePath = "";
            String Tpath = DateTime.Now.ToString("yyyy-MM-dd") + @"/";
            string filename = DateTime.Now.ToFileTime() + file.FileName;
            //string FilePath = "D:\\" + Tpath + filename;              //
            string FilePath = (@"./files/upload/") + Tpath + filename;
            //这里应该获取当前项目路径地址，再在后面创建文件，如果按上面的注释掉的写法，在服务器上没有找到d盘，则会报错。
            string diPath = Path.GetDirectoryName(FilePath);    //获取到当前目录的文件夹，没有就创建

            if (!Directory.Exists(diPath)) { Directory.CreateDirectory(diPath); };
            try
            {
                completePath = FilePath;
                using (Stream stream = file.OpenReadStream())
                using (FileStream fileStream = System.IO.File.Create(completePath, (int)stream.Length))
                {
                    var buffer = new byte[200 * 1024];
                    int count;
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, count);
                    }
                }
                return completePath;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public bool JudgeCol(ExcelWorksheet worksheet,string[] colName)
        {
            int ColCount = worksheet.Dimension.Columns;
            bool bHeaderRow = true;
            //excel下标从1开始
            for (int col = 1,index=0; col <= ColCount&&index<colName.Length; col++,index++)
            {
                string c = worksheet.Cells[1, col].Value.ToString();
                if (!c.Equals(colName[index]))
                {
                    bHeaderRow = false;
                    throw new Exception("格式错误，导入文件的第"+index+"列应为"+colName[index]+"！");
                }
            }
            return bHeaderRow;
        }
        public object SaveDepToDB(ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.Rows;
            int ColCount = worksheet.Dimension.Columns;
            int successCount = 0, badCount = 0;
            List<Deparment> successDeparments = new List<Deparment>();
            List<ExcelDep> data = new List<ExcelDep>();
            var result = new object();
            for (int row = 2; row <= rowCount; row++)
            {
                Deparment deparment = new Deparment();
                ExcelDep excelDep = new ExcelDep();
                for (int col = 1; col <= ColCount; col++)
                {
                    switch (col)
                    {
                        case 1:
                            deparment.CompanyId = _commonAppService.GetCompId(worksheet.Cells[row, col].Value.ToString());
                            excelDep.Company = worksheet.Cells[row, col].Value.ToString(); break;
                        case 2: deparment.ManagerId = _commonAppService.GetUserId(worksheet.Cells[row, col].Value.ToString());
                            excelDep.Manager = worksheet.Cells[row, col].Value.ToString(); break;
                        case 3: deparment.Name = worksheet.Cells[row, col].Value.ToString();
                            excelDep.Name = worksheet.Cells[row, col].Value.ToString(); break;
                        case 4: deparment.WorkerCount = Convert.ToInt32(worksheet.Cells[row, col].Value);
                            excelDep.WorkerCount = Convert.ToInt32(worksheet.Cells[row, col].Value); break;
                        case 5: deparment.Code = worksheet.Cells[row, col].Value.ToString();
                            excelDep.Code = worksheet.Cells[row, col].Value.ToString(); break;
                        default: break;
                    };
                }
                if (deparment.CompanyId != 0 && deparment.ManagerId != 0)
                {
                    if (!_commonAppService.IsExitDep(deparment.Name, deparment.CompanyId))
                    {
                        excelDep.IsSuccess = true;
                        successDeparments.Add(deparment);
                        successCount++;
                    }
                    else
                    {
                        excelDep.IsSuccess = false;
                        badCount++;
                    }
                }
                else
                {
                    excelDep.IsSuccess = false;
                    badCount++;
                }
                data.Add(excelDep);
            }
            _ctx.Deparment.AddRange(successDeparments);
            Company company= _ctx.Company.Find(successDeparments[0].CompanyId);
            company.DeparmentCount = company.DeparmentCount + successCount;
            _ctx.SaveChanges();
            result = new
            {
                successCount,
                badCount,
                data
            };
            return result;
        }
        public object SaveWorkerToDB(ExcelWorksheet worksheet,int rowCount,int colCount)
        {
            int successCount = 0, badCount = 0;
            List<Worker> successWorkers = new List<Worker>();
            List<ExcelWorker> data = new List<ExcelWorker>();
            var result = new object();
            for (int row = 2; row <= rowCount; row++)
            {
                Worker worker = new Worker();
                ExcelWorker excelWorker = new ExcelWorker();
                excelWorker.Account= worker.Account = (DateTime.Now.AddSeconds(10).ToFileTime().ToString()).Substring(6);
                worker.Password = "123456";
                for (int col = 1; col <= colCount; col++)
                {
                    switch (col)
                    {
                        case 1: //公司
                            worker.CompanyId = _commonAppService.GetCompId(worksheet.Cells[row, col].Value.ToString());
                            excelWorker.Company = worksheet.Cells[row, col].Value.ToString(); break;
                        case 2: //部门
                            worker.DepartmentId = _commonAppService.GetDepId(worksheet.Cells[row, col].Value.ToString());
                            excelWorker.Department = worksheet.Cells[row, col].Value.ToString(); break;
                        case 3: //职位
                            worker.PositionId = _commonAppService.GetPosition(worksheet.Cells[row, col].Value.ToString(),worker.CompanyId);
                            excelWorker.Position = worksheet.Cells[row, col].Value.ToString(); break;
                        case 4:  //姓名
                            excelWorker.Name=worker.Name = worksheet.Cells[row, col].Value.ToString(); break;
                        case 5:  //性别
                            worker.Sex = worksheet.Cells[row, col].Value.ToString() == "男" ? 1 : 0;
                            excelWorker.Sex = worksheet.Cells[row, col].Value.ToString(); break;
                        case 6: //电话号码
                            excelWorker.PhoneNumber= worker.PhoneNumber = worksheet.Cells[row, col].Value.ToString();
                            break;
                        case 7:  //地址
                            excelWorker.Address= worker.Address = worksheet.Cells[row, col].Value.ToString();
                            break;
                        case 8: //证件类型
                            worker.PaperType = _commonAppService.GetPaperType(worksheet.Cells[row, col].Value.ToString());
                            excelWorker.PaperType = worksheet.Cells[row, col].Value.ToString();
                            break;
                        case 9: //证件号码
                            excelWorker.PaperNumber= worker.PaperNumber = worksheet.Cells[row, col].Value.ToString();
                            break;
                        case 10: //状态
                            worker.StateId = _commonAppService.GetState(worksheet.Cells[row, col].Value.ToString(), worker.CompanyId);
                            excelWorker.State = worksheet.Cells[row, col].Value.ToString();
                            break;
                        case 11: //入职时间
                            DateTime time = Convert.ToDateTime(worksheet.Cells[row, col].Value.ToString());
                            worker.EntryTime = time.ToFileTime();
                            excelWorker.EntryTime = worksheet.Cells[row, col].Value.ToString();
                            break;
                        default: break;
                    };
                }
                if (worker.CompanyId != 0 && worker.DepartmentId != 0 && worker.PositionId != 0)
                {
                    if (!_commonAppService.IsExitWorker(worker.PaperType,worker.PaperNumber, worker.CompanyId))
                    {
                        excelWorker.IsSuccess = true;
                        worker.Age = _userService.GetAgeFromIdCard(worker.PaperNumber);
                        worker.Brith = _userService.GetBirthdayFromIdCard(worker.PaperNumber);
                        successWorkers.Add(worker);
                        successCount++;
                    }
                    else
                    {
                        excelWorker.IsSuccess = false;
                        badCount++;
                    }
                }
                else
                {
                    excelWorker.IsSuccess = false;
                    badCount++;
                }
                data.Add(excelWorker);
            }
            if (successCount != 0)
            {
                _ctx.Worker.AddRange(successWorkers);
                _commonAppService.ChangeDepWorkerCount(successWorkers);
                Company company = _ctx.Company.Find(successWorkers[0].CompanyId);
                company.WokerCount = company.WokerCount + successCount;
                _ctx.SaveChanges();
            }
            result = new
            {
                successCount,
                badCount,
                data
            };
            return result;
        }
    }
}
