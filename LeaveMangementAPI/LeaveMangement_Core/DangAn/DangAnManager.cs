using LeaveMangement_Entity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LeaveMangement_Core.User;
using LeaveMangement_Entity.Dtos;

namespace LeaveMangement_Core.DangAn
{
    public class DangAnManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private UserManager _userManager = new UserManager();

        public int GetUserCompId(string account)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).CompanyId;
        }
        public int GetUserDepId(string account)
        {
            return _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).DepartmentId;
        }
        public List<Company> GetCompanyList()
        {
            return _ctx.Company.ToList();
        }
        public Company GetCompanyById(int compId)
        {
            return _ctx.Company.Find(compId);
        }
        public object AddCompany(Company company)
        {
            Company comp = _ctx.Company.SingleOrDefault(c => c.Name.Equals(company.Name));
            var result = new object();
            if (comp != null)
                result = new
                {
                    isSuccess = false,
                    message = "公司名字已经存在！"
                };
            else
            {
                //添加公司、自动创建一个管理员账号、发送邮件
                Company newComp = new Company()
                {
                    Name = company.Name,
                    CellphoneNumber = company.CellphoneNumber,
                    Corporation = company.Corporation,
                    Email = company.Email,
                    CreateTime = company.CreateTime
                };
                _ctx.Company.Add(newComp);
                _ctx.SaveChanges();
                if (_userManager.CreateCompanyAdmin(newComp))
                    result = new
                    {
                        isSuccess = true,
                        message = "公司添加成功！"
                    };
                else
                    result = new
                    {
                        isSuccess = false,
                        message = "公司添加失败！"
                    };
            }
            return result;
        }
        public object EditCompany(Company company)
        {
            Company comp = _ctx.Company.Find(company.Id);
            var result = new object();
            try
            {
                comp = company;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "成功修改公司信息！"
                };
            }catch(Exception e)
            {
                result = new
                {
                    isSuccess = true,
                    message = "修改公司信息失败！"
                };
            }
            return result;
        }
        public object DeleteCompany(int compId)
        {
            Company comp = _ctx.Company.Find(compId);
            var result = new object();
            try
            {
                _ctx.Company.Remove(comp);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "删除成功！"
                };
            }
            catch(Exception e)
            {
                result = new
                {
                    isSuccess = true,
                    message = "删除失败！"
                };
            }
            return result;
        }
        public object GetDeparmentList(DepartmentDto query)
        {
            query.Query = string.IsNullOrEmpty(query.Query) ? "" : query.Query;
            //连接表查询经理名称
            var deparments = (from dep in _ctx.Deparment
                              join worker in _ctx.Worker on dep.ManagerId equals worker.Id
                              where dep.CompanyId == query.CompId && dep.Name.Contains(query.Query)
                              select new
                              {
                                  name = dep.Name,
                                  code = dep.Code,
                                  workerCount = dep.WorkerCount,
                                  manger = worker.Name,
                              }).ToList();
            var result = new
            {
                totalCount = deparments.Count(),
                data = deparments.Skip((query.CurrentPage-1) * query.CurrentPageSize ).Take(query.CurrentPageSize),
            };
            return result;
        }
        public object AddSingleDpearment(AddSingleDeparmentDto deparmentDto)
        {
            var dep = _ctx.Deparment.SingleOrDefault(d => d.Name.Equals(deparmentDto)&&d.CompanyId.Equals(deparmentDto.CompId));
            var result = new object();
            if (dep != null)
                result = new
                {
                    isSuccess = false,
                    message = "该部门已存在"
                };
            else
            {
                Deparment deparment = new Deparment()
                {
                    Name = deparmentDto.Name,
                    CompanyId = deparmentDto.CompId,
                    Code = "01",
                    ManagerId = deparmentDto.MangerId,
                    WorkerCount = DangAnHelper.DEFAULT_WORKER_COUNT,
                };
                _ctx.Deparment.Add(deparment);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "添加部门成功"
                };
            }

            return result;
        }
        public object GetWorkerList(WorkDto query)
        {
            query.Query = string.IsNullOrEmpty(query.Query) ? "" : query.Query;
            int defaultNum = 0;
            var result = new object();
            var workers = (from worker in _ctx.Worker
                           join comp in _ctx.Company on worker.CompanyId equals comp.Id
                           join dep in _ctx.Deparment on worker.DepartmentId equals dep.Id
                           join position in _ctx.Position on worker.PositionId equals position.Id
                           where worker.CompanyId.Equals(query.CompId) && (worker.Name.Contains(query.Query) ||
                           (worker.Account.Contains(query.Query)))
                           select new
                           {
                               name = worker.Name,
                               account = worker.Account,
                               company = comp.Name,
                               deparment = dep.Name,
                               deparmentId = worker.Id,
                               position = position.Name,
                               age = worker.Age,
                               address = worker.Address,
                               phoneNumber = worker.PaperNumber,
                               sex = worker.Sex==0?"女":"男",
                           }).ToList();
            if (query.DepId != defaultNum)
                workers = workers.Where(w => w.deparmentId == query.DepId).ToList();
            result = new
            {
                totalCount = workers.Count(),
                data = workers.Skip((query.CurrentPage - 1) * query.CurrentPageSize).Take(query.CurrentPageSize)
            };
            return result;
        }
    }
}
