using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangement_Entity.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using LeaveMangement_Core.Common;
using LeaveMangement_Entity.Dtos;

namespace LeaveMangement_Core.Approval
{
    public class ApprovalManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private ApprovalService _approvalService = new ApprovalService();
        private CommonServer _commonServer = new CommonServer();
        private CommonManager _commonManager = new CommonManager();

        public int GetApprovalCount(string account, int compid)
        {
            return _commonManager.GetLeaveCount(account, compid);
            //int count = 0;
            //int workerId = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account) && w.CompanyId == compid).Id;
            //DateTime now = DateTime.Now;
            //int nowYear = now.Year;
            //int nowMonth = now.Month;
            //var result = _ctx.Apply.Where(a => a.WorkerId == workerId).ToList();
            //foreach(var item in result)
            //{
            //    if(DateTime.FromFileTime(item.StartTime).Year == nowYear && DateTime.FromFileTime(item.StartTime).Month == nowMonth)
            //        count++;
            //}
            //return count;
        }
        public object AddApplication(AddApplicationDto addApplicationDto)
        {
            var result = new object();
            var application = _ctx.Apply.SingleOrDefault(a => a.WorkerId == addApplicationDto.WorkerId &&
            a.StartTime <= addApplicationDto.StartTime && a.EndTime >= addApplicationDto.EndTime);
            if (application != null)
                result = new
                {
                    isSuccess = false,
                    message = "您已有该时间段的假期！"
                };
            else
            {
                Apply newApply = new Apply()
                {
                    WorkerId = addApplicationDto.WorkerId,
                    DeparmentId = addApplicationDto.DeparmentId,
                    CompanyId = addApplicationDto.CompId,
                    Type1 = addApplicationDto.Type1,
                    Type2 = addApplicationDto.Type2,
                    Account = addApplicationDto.Account,
                    State = ApprovalHelper.DEFAULT_APPROVAL_STATE,
                    IsSubmit = addApplicationDto.IsSubmit,
                    StartTime = addApplicationDto.StartTime,
                    EndTime = addApplicationDto.EndTime,
                    CreateTime = DateTime.Now.ToFileTime(),
                    IsRevoke = false,
                };
                _ctx.Apply.Add(newApply);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "申请已保存！"
                };
                //添加完成后添加通知
                if (newApply.State == 1&&newApply.IsSubmit)
                {
                    AddInform(newApply.Id,newApply.WorkerId,"Leave");
                }
            }
            return result;
        }
        private void AddInform(int applyId,int id,string type)
        {
            //找到当前提交申请员工的上级用户
            int deparmentId = _ctx.Worker.Find(id).DepartmentId;
            if (_ctx.Deparment.Find(deparmentId).ManagerId == null)
                throw new Exception("部门没有部门经理");
            int mangerId = (int)_ctx.Deparment.Find(deparmentId).ManagerId;
            int workerId = 0;
            //判断经理的ID是否等于提交申请的用户的ID,不等于则直接创建通知。等于就将通知转给上级
            if (mangerId != id)
                workerId = mangerId;
            else
            {
                //找到公司的上级的编号
                //int positionId = _ctx.Position.SingleOrDefault(p => p.CompanyId == apply.CompanyId).ParentId;
                var position = (from p in _ctx.Position
                                  join w in _ctx.Worker on p.Id equals w.PositionId
                                  where w.Id == mangerId
                                  select p).First();
                int positionId = position.ParentId == 0 ? position.Id : position.ParentId;
                workerId = _ctx.Worker.SingleOrDefault(w => w.PositionId == positionId).Id;
            }
            //找出上级职位编号
            //添加通知
            Inform inform = new Inform()
            {
                WorkId = (int)workerId,
                ApplicationId = applyId,
                IsLook = false,
                CreateTime = DateTime.Now.ToFileTime(),
                Content = "待审核的申请",
                Type = type
            };
            _ctx.Inform.Add(inform);
            _ctx.SaveChanges();
        }
        //获取提交申请列表
        public object GetApplicationList(GetApplicationListDto getApplicationListDto)
        {
            var worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(getApplicationListDto.Account));
            var list = (from apply in _ctx.Apply
                        join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                        where apply.WorkerId == worker.Id&&apply.IsSubmit&&!apply.IsRevoke &&apply.Account.Contains(getApplicationListDto.Query)
                        select new { 
                            id = apply.Id,
                            workerName = worker.Name,
                            deparment = deparment.Name,
                            type = _approvalService.GetApplicationType(apply.Type1,apply.Type2),
                            state = apply.State,
                            stateName = _approvalService.GetStateName(apply.State),
                            startTime = apply.StartTime,
                            endTime = apply.EndTime,
                            createTime = _commonServer.ChangeTime(apply.CreateTime),
                        }).ToList();
            var result = new object();
            result = new
            {
                count = list.Count(),
                data = list.Skip((getApplicationListDto.CurrentPage - 1) * getApplicationListDto.CurrentPageSize).Take(getApplicationListDto.CurrentPageSize),
            };
            return result;
        }
        public object GetApplicationById(int id)
        {
            var application = _ctx.Apply.Find(id);
            var list = new object();
            if(application.LeaderId == null)
            {
                list = (from apply in _ctx.Apply
                        join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                        join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                        where apply.Id == id & !apply.IsRevoke
                        select new
                        {
                            workerName = worker.Name,
                            deparment = deparment.Name,
                            account = apply.Account,
                            type = _approvalService.GetApplicationType(apply.Type1, apply.Type2),
                            state = apply.State,
                            stateName = apply.State == 0?"":_approvalService.GetStateName(apply.State),
                            startTime = apply.StartTime,
                            endTime = apply.EndTime,
                            remark = "",
                            handerName = "",
                            handerTime = "",
                            createTime = _commonServer.ChangeTime(apply.CreateTime),
                        }).FirstOrDefault();
            }
            else
            {
                list = (from apply in _ctx.Apply
                        join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                        join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                        where apply.Id == id &&!apply.IsRevoke
                        select new
                        {
                            workerName = worker.Name,
                            deparment = deparment.Name,
                            account = apply.Account,
                            type = _approvalService.GetApplicationType(apply.Type1, apply.Type2),
                            state = apply.State,
                            stateName = _approvalService.GetStateName(apply.State),
                            startTime = apply.StartTime,
                            endTime = apply.EndTime,
                            remark = apply.Remark,
                            handerName = GetHanderName(apply.LeaderId),
                            handerTime = _commonServer.ChangeTime((long)apply.HandleTime),
                            createTime = _commonServer.ChangeTime(apply.CreateTime),
                        }).FirstOrDefault();
            }
            
            return list;
        }
        public object GetUnApplicationList(GetApplicationListDto getApplicationListDto)
        {
            var worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(getApplicationListDto.Account));
            var list = (from apply in _ctx.Apply
                        join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                        where apply.WorkerId == worker.Id && !apply.IsSubmit && apply.Account.Contains(getApplicationListDto.Query)
                        select new
                        {
                            id = apply.Id,
                            workerName = worker.Name,
                            deparment = deparment.Name,
                            type = _approvalService.GetApplicationType(apply.Type1, apply.Type2),
                            startTime = apply.StartTime,
                            endTime = apply.EndTime,
                            createTime = _commonServer.ChangeTime(apply.CreateTime),
                        }).ToList();
            var result = new object();
            result = new
            {
                count = list.Count(),
                data = list.Skip((getApplicationListDto.CurrentPage - 1) * getApplicationListDto.CurrentPageSize).Take(getApplicationListDto.CurrentPageSize),
            };
            return result;
        }
        public object SubmitApplication(int id)
        {
            var result = new object();
            try
            {
                var application = _ctx.Apply.Find(id);
                application.IsSubmit = true;
                application.State = 1;
                _ctx.SaveChanges();
                AddInform(application.Id,application.WorkerId,"Leave");
                result = new
                {
                    isSuccess = true,
                    message = "提交申请成功！",
                };                
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "提交申请失败！",
                };
            }
            return result;
        }
        public object RevokeApplication(int id)
        {
            var result = new object();
            Apply apply = _ctx.Apply.Find(id);
            apply.IsRevoke = true;
            _ctx.SaveChanges();
            UpdateUserState(apply.CompanyId, apply.WorkerId, "在职");
            result = new
            {
                isSuccess = true,
                message = "您已销假成功！",
            };
            return result;
        }
        public object EditApplication(EditApplicationDto editApplicationDto)
        {
            var application = _ctx.Apply.Find(editApplicationDto.Id);
            var result = new object();
            try
            {
                application.Type1 = editApplicationDto.Type1;
                application.Type2 = editApplicationDto.Type2;
                application.Account = editApplicationDto.Account;
                application.StartTime = editApplicationDto.StartTime;
                application.EndTime = editApplicationDto.EndTime;
                application.IsSubmit = editApplicationDto.IsSubmit;
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "申请编辑成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "申请编辑失败！",
                };
            }
            return result;
        }
        public object DeleteApplicationById(int id)
        {
            Apply apply = _ctx.Apply.Find(id);
            var result = new object();
            try
            {
                _ctx.Apply.Remove(apply);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "申请删除成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "申请删除失败！",
                };
            }
            return result;
        }

        public object GetInform(string account)
        {
            int workerId = _ctx.Worker.SingleOrDefault(w=>w.Account.Equals(account)).Id;
            return _ctx.Inform.Where(i => i.WorkId == workerId).ToList();
        }
        public object GetCheckingList(CheckingDto checkingDto)
        {
            //从通知表里找出该用户需要审核的条目
            int workerId = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(checkingDto.Account) && w.CompanyId == checkingDto.CompId).Id;
            var applicationIds = (from info in _ctx.Inform
                                   where info.WorkId == workerId && info.Type.Equals("Leave")
                                   select info.ApplicationId).ToArray();
            //再找出请假申请列表
            var applications = (from apply in _ctx.Apply
                               join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                               join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                               where applicationIds.Contains(apply.Id) && worker.Name.Contains(checkingDto.Query)
                               select new
                               {
                                   id = apply.Id,
                                   workerName = worker.Name,
                                   deparment = deparment.Name,
                                   type = "Leave",
                                   typeName = _approvalService.GetApplicationType(apply.Type1, apply.Type2),
                                   state = apply.State,
                                   stateName = _approvalService.GetStateName(apply.State),
                                   startTime = apply.StartTime,
                                   endTime = apply.EndTime,
                                   content = apply.Account,
                                   createTime = _commonServer.ChangeTime(apply.CreateTime),
                               }).ToList();
            var result = new object();
            result = new
            {
                count = applications.Count(),
                data = applications.Skip((checkingDto.CurrentPage - 1) * checkingDto.CurrentPageSize).Take(checkingDto.CurrentPageSize),
            };
            return result;
        }
        public object GetApplyJobList(CheckingDto checkingDto)
        {
            //从通知表里找出该用户需要审核的条目
            int workerId = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(checkingDto.Account) && w.CompanyId == checkingDto.CompId).Id;
            var applyIds = (from info in _ctx.Inform
                                  where info.WorkId == workerId && info.Type.Equals("Job")
                                  select info.ApplicationId).ToArray();
            //再找出申请列表
            var applys = (from apply in _ctx.ApplyFoJob
                                join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                                join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                                where applyIds.Contains(apply.Id) && worker.Name.Contains(checkingDto.Query)
                                select new
                                {
                                    id = apply.Id,
                                    workerName = worker.Name,
                                    deparment = deparment.Name,
                                    type = "Job",
                                    type1 = apply.Type,
                                    typeName = apply.Type.Equals("correction")?"转正申请":"离职申请",
                                    content = apply.Content,
                                    createTime = _commonServer.ChangeTime(apply.CreateTime),
                                }).ToList();
            var result = new object();
            result = new
            {
                count = applys.Count(),
                data = applys.Skip((checkingDto.CurrentPage - 1) * checkingDto.CurrentPageSize).Take(checkingDto.CurrentPageSize),
            };
            return result;

        }
        public object CheckApplication(CheckDto checkDto, string account)
        {
            var result = new object();
            try
            {
                //找出处理人的ID
                Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
                Inform inform = _ctx.Inform.SingleOrDefault(i => i.ApplicationId == checkDto.ApplicationId);
                switch (checkDto.Type)
                {
                    case "Leave":
                        CheckApplication(worker, checkDto);break;
                    case "Job": CheckApply(worker,checkDto); break;
                }
                //删除待审核的通知记录
                _ctx.Inform.Remove(inform);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "审核成功！",
                };
            }
            catch
            {
                result = new
                {
                    isSuccess = false,
                    message = "审核失败！",
                };
            }
            return result;
        }
        private void CheckApplication(Worker worker,CheckDto checkDto)
        {
            //找出申请
            Apply apply = _ctx.Apply.Find(checkDto.ApplicationId);
            string remark = worker.Name + "：" + checkDto.Remark + ";" + apply.Remark;
            //更新申请的处理信息
            apply.HandleTime = DateTime.Now.ToFileTime();
            apply.LeaderId = worker.Id;
            apply.Remark = remark;
            apply.State = checkDto.IsAgree ? 2 : 3;
            _ctx.SaveChanges();
            //更新员工的状态
            UpdateUserState(apply.CompanyId, apply.WorkerId, "休假");
        }
        private void CheckApply(Worker worker, CheckDto checkDto)
        {
            string state = "";
            ApplyFoJob apply = _ctx.ApplyFoJob.Find(checkDto.ApplicationId);
            string stateName = _ctx.State.Find(worker.StateId).Name;
            apply.HandlerId = worker.Id;
            apply.HandlTime = DateTime.Now.ToFileTime();
            apply.Result = checkDto.IsAgree;
            _ctx.SaveChanges();
            state = checkDto.IsAgree && apply.Type== "correction" ? "在职" : stateName;
            state = checkDto.IsAgree && apply.Type == "separation" ? "离职" : stateName;
            bool isDelete = checkDto.IsAgree ? false : true;
            UpdateUserState(apply.CompanyId, apply.WorkerId,state,isDelete);
        }
        public object PushCheck(PushCheck pushCheck, string account)
        {
            var result = new object();
            Apply apply = _ctx.Apply.Find(pushCheck.ApplicationId);
            //找出当前用户的职位编号
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
            //根据职位编号找出职位
            Position position = _ctx.Position.Find(worker.PositionId);
            string remark = worker.Name + "：" + pushCheck.Remark + ";" + apply.Remark;
            apply.Remark = remark;
            if (position.ParentId == 0)
                result = new
                {
                    isSuccess = false,
                    message = "提交失败，您目前没有上级管理！",
                };
            else
            {
                Inform inform = new Inform()
                {
                    WorkId = position.ParentId,
                    ApplicationId = pushCheck.ApplicationId,
                    IsLook = false,
                    CreateTime = DateTime.Now.ToFileTime(),
                    Content = "待审核的申请",
                };
                Inform oldInfo = _ctx.Inform.SingleOrDefault(i => i.WorkId == worker.Id);
                _ctx.Inform.Remove(oldInfo);
                _ctx.Inform.Add(inform);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "提交成功！",
                };
            }
            return result;
        }
        //获取处理申请人姓名
        public string GetHanderName(int? id)
        {
            try
            {
                return _ctx.Worker.Find(id).Name;
            }
            catch
            {
                return "";
            }            
        }
        public void UpdateUserState(int companyId,int workerId,string stateName,bool isDelete = false)
        {
            State state = _ctx.State.SingleOrDefault(s => s.CompanyId == companyId && s.Name.Contains(stateName));
            Worker worker = _ctx.Worker.Find(workerId);
            worker.StateId = state.Id;
            if (isDelete)
            {
                Company company = _ctx.Company.Find(worker.CompanyId);
                int workerCount = company.WokerCount - 1 <= 0 ? 0 : company.WokerCount - 1;
                company.WokerCount = workerCount;
            }
            _ctx.SaveChanges();
        }

        public Result CreateApplyOfJob(ApplyJobDto applyJobDto, string account)
        {
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
            ApplyFoJob apply = new ApplyFoJob()
            {
                Content = applyJobDto.Content,
                Type = applyJobDto.Type,
                CreateTime = DateTime.Now.ToFileTime(),
                WorkerId = worker.Id,
                DeparmentId = worker.DepartmentId,
                CompanyId = worker.CompanyId,
            };
            _ctx.ApplyFoJob.Add(apply);
            _ctx.SaveChanges();
            AddInform(apply.Id,apply.WorkerId, "Job");
            Result result = new Result()
            {
                IsSuccess = true,
                Message = "添加成功！"
            };
            return result;
        }
    }
}
