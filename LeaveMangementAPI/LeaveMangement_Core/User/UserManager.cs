using LeaveMangement_Entity.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Core.Common;

namespace LeaveMangement_Core.User
{
    public class UserManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private UserService _userService = new UserService();
        private CommonServer _commonServer = new CommonServer();
        public Worker Login(string account, string password)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(u => u.Account.Equals(account) && u.Password.Equals(password));
            return worker ?? null;
        }
        public bool CreateCompanyAdmin(Company company)
        {
            string account = _userService.CreateAdmAccount();
            Worker user = new Worker()
            {
                Account = account,
                Name = company.Corporation,
                CompanyId = company.Id,
                PositionId = 0,
                DepartmentId = 0,
                Password = UserHelper.DEFAULT_WORKER_PASSWORD,
            };
            _ctx.Worker.Add(user);
            _ctx.SaveChanges();
            return _userService.SendEMail(company.Email, user);
        }
        /// <summary>
        /// 单个添加员工
        /// 如果添加员工的职位为部门经理，则将对应部门的经理改为员工编号
        /// </summary>
        /// <param name="singleWorkerDto"></param>
        /// <returns></returns>
        public object AddSingleWorker(SingleWorkerDto singleWorkerDto)
        {
            var worker = _ctx.Worker.SingleOrDefault(w => w.PaperType.Equals(singleWorkerDto.PaperType) &&
            w.PaperNumber.Equals(singleWorkerDto.PaperNumber));
            var result = new object();
            if (worker != null)
                result = new
                {
                    isSuccess = false,
                    message = "该员工已存在"
                };
            else
            {
                string account = _userService.CreateAdmAccount();
                Worker newWorker = new Worker()
                {
                    Name = singleWorkerDto.Name,
                    Account = account,
                    Password = UserHelper.DEFAULT_WORKER_PASSWORD,
                    CompanyId = singleWorkerDto.CompanyId,
                    PositionId = singleWorkerDto.PositionId,
                    DepartmentId = singleWorkerDto.DepartmentId,
                    Sex = singleWorkerDto.Sex,
                    PaperType = singleWorkerDto.PaperType,
                    PaperNumber = singleWorkerDto.PaperNumber,
                    EntryTime = _commonServer.ConvertTime( (long)singleWorkerDto.EntryTime),
                    StateId = singleWorkerDto.State,
                    IsAuth = UserHelper.DEFAULT_IS_AUTH,
                    Age = _userService.GetAgeFromIdCard(singleWorkerDto.PaperNumber),
                    Brith = _userService.GetBirthdayFromIdCard(singleWorkerDto.PaperNumber)
                };
                _ctx.Worker.Add(newWorker);
                _ctx.SaveChanges();
                _ctx.Company.Find(singleWorkerDto.CompanyId).WokerCount++;
                var dep = _ctx.Deparment.Find(singleWorkerDto.DepartmentId);
                if (_ctx.Position.SingleOrDefault(p => p.Id == singleWorkerDto.PositionId).Name.Contains("部门经理"))
                    dep.ManagerId = newWorker.Id;
                dep.WorkerCount++;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "添加成功！"
                };
            }
            return result;
        }
        public object GetWorkerById(int userId)
        {
            var user = (from worker in _ctx.Worker
                        join comp in _ctx.Company on worker.CompanyId equals comp.Id
                        join dep in _ctx.Deparment on worker.DepartmentId equals dep.Id
                        join position in _ctx.Position on worker.PositionId equals position.Id
                        join paperType in _ctx.PaperType on worker.PaperType equals paperType.Id
                        join state in _ctx.State on worker.StateId equals state.Id
                        where worker.Id == userId
                        select new
                        {
                            name = worker.Name,
                            account = worker.Account,
                            company = comp.Name,
                            deparment = dep.Name,
                            deparmentId = worker.DepartmentId,
                            positionId = position.Id,
                            position = position.Name,
                            age = worker.Age,
                            address = worker.Address,
                            phoneNumber = worker.PhoneNumber,
                            sex = worker.Sex == 0 ? "女" : "男",
                            entryTime = _commonServer.MilliTimeStamp( (long)worker.EntryTime),
                            birth =  _commonServer.MilliTimeStamp( (long)worker.Brith),
                            paperType = paperType.Name,
                            paperNumber = worker.PaperNumber,
                            stateId = state.Id,
                            state = state.Name,
                            isAuth = worker.IsAuth,
                        }).Single();
            return user;
        }
        public object ModifyPassword(ModifyPasswordDto modifyPasswordDto)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Id == modifyPasswordDto.Id && w.Password.Equals(modifyPasswordDto.Password));
            var result = new object();
            if (worker != null)
            {
                worker.Password = modifyPasswordDto.NewPassword;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "修改密码成功！"
                };
            } else
                result = new
                {
                    isSuccess = false,
                    message = "修改密码失败！"
                };
            return result;
        }
        //根据当前用户的职位ID去获取目录
        public List<Menus> GetMenu(int parentId,int positionId)
        {
            //找出根目录
            List<Menus> parents = (from menu in _ctx.Menu
                                   where menu.ParentId == parentId
                                   select new Menus
                                   {
                                       Id = menu.Id,
                                       Name = menu.Name,
                                       PositionId = menu.PositionId,
                                       Icon = menu.Icon,
                                       Url = menu.Url,
                                   }).ToList();
            List<Menus> menus = new List<Menus>();
            foreach (Menus menu in parents)
            {
                int[] positionIds = StringToInt(menu.PositionId);
                if (Array.IndexOf(positionIds,positionId) != -1)   //-1不存在
                {
                    Menus menuNode = new Menus()
                    {
                        Id = menu.Id,
                        Name = menu.Name,
                        PositionId = menu.PositionId,
                        Url = menu.Url,
                        Icon = menu.Icon,
                        Children = GetMenu( menu.Id, positionId)
                    };
                    menus.Add(menuNode);
                }                
            };
            return menus;
        } 
        private int[] StringToInt(string positionId)
        {
            string[] positionIdStr = positionId.Split(',');
            int[] positionIds = new int[positionIdStr.Length];
            for(int i = 0; i < positionIdStr.Length; i++)
            {
                positionIds[i] = int.Parse(positionIdStr[i]);
            }
            return positionIds;
        }
        public object EditUserMessage(EditUserMessageDto editUserMessageDto)
        {
            var result = new object();
            try
            {
                Worker worker = _ctx.Worker.Find(editUserMessageDto.Id);
                worker.PhoneNumber = editUserMessageDto.PhoneNumber;
                worker.Age = editUserMessageDto.Age;
                worker.Address = editUserMessageDto.Address;
                worker.Brith = editUserMessageDto.Birth;
                worker.IsAuth = true;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "完善资料成功！"
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "完善资料失败！"
                };
            }
            return result;
            
        }
    }
}
