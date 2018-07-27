using LeaveMangement_Entity.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using LeaveMangement_Core.User;
using LeaveMangement_Entity.Dtos.DangAn;

namespace LeaveMangement_Core.DangAn
{
    public class DangAnManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private UserManager _userManager = new UserManager();
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
                    CreateTime = company.CreateTime,
                    Address = company.Address,
                    WokerCount = company.WokerCount,
                    DeparmentCount = company.DeparmentCount,
                };
                _ctx.Company.Add(newComp);
                if (_userManager.CreateCompanyAdmin(newComp))
                {
                    
                    _ctx.SaveChanges();
                    result = new
                    {
                        isSuccess = true,
                        message = "公司添加成功！"
                    };
                }                    
                else
                    result = new
                    {
                        isSuccess = false,
                        message = "公司添加失败！"
                    };
            }
            return result;
        }
        public object EditCompany(EditCompanyDto editCompanyDto)
        {
            Company comp = _ctx.Company.Find(editCompanyDto.CompId);
            var result = new object();
            try
            {
                comp.CellphoneNumber = editCompanyDto.CellphoneNumber;
                comp.Corporation = editCompanyDto.Corporation;
                comp.Address = editCompanyDto.Address;
                comp.Name = editCompanyDto.Name;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "成功修改公司信息！"
                };
            }
            catch (Exception e)
            {
                result = new
                {
                    isSuccess = false,
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
            catch (Exception e)
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
                                  id = dep.Id,
                                  name = dep.Name,
                                  code = dep.Code,
                                  workerCount = dep.WorkerCount,
                                  manger = worker.Name,
                              }).ToList();
            var result = new
            {
                totalCount = deparments.Count(),
                data = deparments.Skip((query.CurrentPage - 1) * query.CurrentPageSize).Take(query.CurrentPageSize),
            };
            return result;
        }
        //员工列表时，获取部门选择器数据
        public object GetDeparments(int compId)
        {
            return _ctx.Deparment.Where(d => d.CompanyId == compId);
        }
        public object AddSingleDpearment(AddSingleDeparmentDto deparmentDto)
        {
            var dep = _ctx.Deparment.SingleOrDefault(d => d.Name.Equals(deparmentDto) && d.CompanyId.Equals(deparmentDto.CompId));
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
                    WorkerCount = deparmentDto.WorkerCount,
                };
                if (ChangeWorkerPosition(deparmentDto.MangerId, "部门经理"))
                {
                    _ctx.Deparment.Add(deparment);
                    _ctx.Company.Find(deparmentDto.CompId).DeparmentCount++;
                    _ctx.SaveChanges();
                    result = new
                    {
                        isSuccess = true,
                        message = "添加部门成功"
                    };
                }
                else
                    result = new
                    {
                        isSuccess = false,
                        message = "部门添加失败"
                    };
            }
            return result;
        }
        public object DeleteDeparment(int id)
        {
            Deparment deparment = _ctx.Deparment.Find(id);
            var result = new object();
            try
            {
                if (deparment.WorkerCount != 0)
                    result = new
                    {
                        isSuccess = false,
                        message = "部门内有员工，删除失败！"
                    };
                else
                {
                    if (ChangeWorkerPosition(deparment.ManagerId, "员工"))
                    {
                        _ctx.Company.Find(deparment.CompanyId).DeparmentCount--;
                        _ctx.Deparment.Remove(deparment);
                        _ctx.SaveChanges();
                        result = new
                        {
                            isSuccess = true,
                            message = "部门删除成功！"
                        };
                    }
                    else
                        result = new
                        {
                            isSuccess = false,
                            message = "部门删除失败！"
                        };
                }

            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "删除部门失败！"
                };
            }
            return result;
        }
        public object GetDeparmentById(int id)
        {
            return _ctx.Deparment.Find(id);
        }
        public object EditDeparment(AddSingleDeparmentDto addSingleDeparmentDto)
        {
            var result = new object();
            Deparment deparment = _ctx.Deparment.Find(addSingleDeparmentDto.Id);
            try
            {
                //若新的部门经理为总经理、部门内员工则部门人数不变，否则部门人数+1,原来的部门人数-1
                string isAddCount = ChangeWorkerPosition(deparment, addSingleDeparmentDto.MangerId);
                switch (isAddCount)
                {
                    case "notAdd":
                        deparment.Name = addSingleDeparmentDto.Name;
                        deparment.ManagerId = addSingleDeparmentDto.MangerId;
                        break;
                    case "add":
                        deparment.Name = addSingleDeparmentDto.Name;
                        deparment.ManagerId = addSingleDeparmentDto.MangerId;
                        deparment.WorkerCount++;
                        break;
                }
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "编辑部门资料成功！"
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "编辑部门资料失败！"
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
                           join state in _ctx.State on worker.StateId equals state.Id
                           where worker.CompanyId.Equals(query.CompId) && (worker.Name.Contains(query.Query) ||
                           (worker.Account.Contains(query.Query)))
                           select new
                           {
                               id = worker.Id,
                               name = worker.Name,
                               company = comp.Name,
                               deparment = dep.Name,
                               deparmentId = dep.Id,
                               position = position.Name,
                               paperNumber = worker.PaperNumber,
                               entryTime = worker.EntryTime,
                               state = state.Name,
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
        //添加部门时，选择经理的下拉列表
        public object GetWorkerList(int compId)
        {
            int[] positionIds = _ctx.Position.Where(u => u.Name.Equals("部门经理")).Select(p => p.Id).ToArray();
            var workers = (from worker in _ctx.Worker
                           join position in _ctx.Position on worker.PositionId equals position.Id
                           where worker.CompanyId.Equals(compId) && !position.Name.Equals("部门经理")
                           select new
                           {
                               id = worker.Id,
                               name = worker.Name,
                               position = position.Name,
                           }).ToList();
            return workers;
        }
        //获取登录用户所在的公司的职位列表
        public object GetPositionListByCompId(int compId)
        {
            var positions = _ctx.Position.Where(p => p.CompanyId == compId).ToList();
            return positions;
        }
        public object DeletePosition(int id)
        {

            Position position = _ctx.Position.Find(id);
            var result = new object();
            try
            {
                //删除职位后，该职位的所有员工，职位更改为员工(workerId)
                int workerId = _ctx.Position.SingleOrDefault(p => p.CompanyId == position.CompanyId && p.Name.Equals("员工")).Id;
                List<Worker> workers = _ctx.Worker.Where(w => w.PositionId == workerId).ToList();
                foreach (Worker worker in workers)
                {
                    worker.PositionId = workerId;
                }
                _ctx.Position.Remove(position);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "删除成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "删除失败！",
                };
            }
            return result;
        }
        public object AddPosition(AddStateDto addStateDto)
        {
            Position position = _ctx.Position.SingleOrDefault(p => p.CompanyId == addStateDto.CompId && p.Name.Equals(addStateDto.Name));
            var result = new object();
            if (position != null)
                result = new
                {
                    isSuccess = false,
                    message = "该职位已经存在！",
                };
            else
            {
                Position newPosition = new Position()
                {
                    Name = addStateDto.Name,
                    Pay = addStateDto.Pay,
                    CompanyId = addStateDto.CompId,
                    ParentId = addStateDto.ParentId,
                };
                try
                {
                    _ctx.Position.Add(newPosition);
                    _ctx.SaveChanges();
                    result = new
                    {
                        isSuccess = true,
                        message = "添加职位成功！",
                    };
                }
                catch
                {
                    result = new
                    {
                        isSuccess = false,
                        message = "添加失败！",
                    };
                }

            }
            return result;
        }
        public object EditPosition(AddStateDto addStateDto)
        {
            Position position = _ctx.Position.Find(addStateDto.Id);
            var result = new object();
            try
            {
                position.Name = addStateDto.Name;
                position.Pay = addStateDto.Pay;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "编辑成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "编辑失败！",
                };
            }
            return result;
        }
        //获取登录用户所在的公司的员工状态列表
        public object GetStateListByCompId(int compId)
        {
            return _ctx.State.Where(s => s.CompanyId == compId).ToList();
        }
        public object DeleteState(int id)
        {
            State state = _ctx.State.Find(id);
            var result = new object();
            try
            {
                _ctx.State.Remove(state);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "删除成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "删除失败！",
                };
            }
            return result;
        }
        public object AddState(AddStateDto addStateDto)
        {
            State state = _ctx.State.SingleOrDefault(p => p.CompanyId == addStateDto.CompId && p.Name.Equals(addStateDto.Name));
            var result = new object();
            if (state != null)
                result = new
                {
                    isSuccess = false,
                    message = "该状态已经存在！",
                };
            else
            {
                State newState = new State()
                {
                    Name = addStateDto.Name,
                    Pay = addStateDto.Pay,
                    CompanyId = addStateDto.CompId
                };
                try
                {
                    _ctx.State.Add(newState);
                    _ctx.SaveChanges();
                    result = new
                    {
                        isSuccess = true,
                        message = "添加员工状态成功！",
                    };
                }
                catch
                {
                    result = new
                    {
                        isSuccess = false,
                        message = "添加失败！",
                    };
                }

            }
            return result;
        }
        public object EditState(AddStateDto addStateDto)
        {
            State state = _ctx.State.Find(addStateDto.Id);
            var result = new object();
            try
            {
                state.Name = addStateDto.Name;
                state.Pay = addStateDto.Pay;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "编辑成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "编辑失败！",
                };
            }
            return result;
        }
        private string ChangeWorkerPosition(Deparment deparment, int newMangerId)
        {
            //将原本的部门经理的职位调整为员工
            Worker oldManger = _ctx.Worker.Find(deparment.ManagerId);
            oldManger.StateId = _ctx.Position.SingleOrDefault(p => p.CompanyId == deparment.CompanyId && p.Name.Equals("员工")).Id;
            //将选择的managerId对应的员工的职位调整为经理职位，为总经理则职位不变
            Worker newManger = _ctx.Worker.Find(newMangerId);
            if (_ctx.Position.Find(newManger.StateId).Name.Equals("总经理") || newManger.DepartmentId == deparment.Id)
            {
                _ctx.SaveChanges();
                return "notAdd";
            }
            else
            {
                newManger.PositionId = _ctx.Position.SingleOrDefault(p => p.CompanyId == deparment.CompanyId && p.Name.Equals("部门经理")).Id;
                Deparment oldDeparment = _ctx.Deparment.Find(newManger.DepartmentId);
                int workerCount = oldDeparment.WorkerCount;
                oldDeparment.WorkerCount = workerCount==0?0:workerCount-1;
                _ctx.SaveChanges();
                return "add";
            }
        }
        //添加部门时，改变员工职位z,总经理不变
        private bool ChangeWorkerPosition(int? managerId, string depName)
        {
            Worker worker = _ctx.Worker.Find(managerId);
            //判断选择的经理原本是否为总经理
            Position manager = _ctx.Position.SingleOrDefault(p => p.Id == worker.PositionId);
            if (manager.Name.Equals("总经理"))
            {
                return true;
            }
            else
            {
                //找到所选公司的部门经理的职位
                Position depManager = _ctx.Position.SingleOrDefault(p => p.CompanyId == worker.CompanyId && p.Name.Equals(depName));
                worker.PositionId = depManager.Id;
                _ctx.SaveChanges();
                return true;
            }

        }
    }
}
