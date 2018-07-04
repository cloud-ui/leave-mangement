using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangement_Entity.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using LeaveMangement_Core.Common;

namespace LeaveMangement_Core.Approval
{
    public class ApprovalManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private ApprovalService _approvalService = new ApprovalService();
        private CommonServer _commonServer = new CommonServer();

        public int GetApprovalCount(string account, int compid)
        {
            int count = 0;
            int workerId = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account) && w.CompanyId == compid).Id;
            DateTime now = DateTime.Now;
            int nowYear = now.Year;
            int nowMonth = now.Month;
            var result = _ctx.Apply.Where(a => a.WorkerId == workerId).ToList();
            foreach(var item in result)
            {
                if(DateTime.FromFileTime(item.StartTime).Year == nowYear && DateTime.FromFileTime(item.StartTime).Month == nowMonth)
                    count++;
            }
            return count;
        }
        public object AddApplication(AddApplicationDto addApplicationDto)
        {
            var result = new object();
            var application = _ctx.Apply.SingleOrDefault(a => a.WorkerId == addApplicationDto.WorkerId &&
            a.StartTime <= addApplicationDto.StartTime && a.EndTime >= addApplicationDto.EndTime);
            //状态
            int state = addApplicationDto.IsSubmit ? ApprovalHelper.DEFAULT_APPROVAL_STATE : 0;
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
                };
                _ctx.Apply.Add(newApply);
                _ctx.SaveChanges();
                result = new
                {
                    isSuccess = true,
                    message = "申请已保存！"
                };
                //添加完成后添加通知
                AddInform(newApply);
            }
            return result;
        }
        private void AddInform(Apply apply)
        {
            //找到当前提交申请员工的上级用户
            int deparmentId = _ctx.Worker.Find(apply.WorkerId).DepartmentId;
            int mangerId = _ctx.Deparment.Find(deparmentId).ManagerId;
            int workerId = 0;
            //判断经理的ID是否等于提交申请的用户的ID,不等于则直接创建通知。等于就将通知转给总经理
            if (mangerId != apply.WorkerId)
                workerId = mangerId;
            else
            {
                //找到公司的总经理的编号
                int positionId = _ctx.Position.SingleOrDefault(p => p.CompanyId == apply.CompanyId && p.Name.Equals("总经理")).Id;
                workerId = _ctx.Worker.SingleOrDefault(w => w.PositionId == positionId).Id;
            }
            //添加通知
            Inform inform = new Inform()
            {
                WorkId = workerId,
                ApplicationId = apply.Id,
                IsLook = false,
                CreateTime = DateTime.Now.ToFileTime(),
                Content = "待审核的申请",
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
                        where apply.WorkerId == worker.Id&&apply.IsSubmit&&apply.Account.Contains(getApplicationListDto.Query)
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
            var list = (from apply in _ctx.Apply
                        join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                        join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                        where apply.Id == id
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
                        }).ToList();
            return list[0];
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
            int[] applicationIds = (from info in _ctx.Inform
                                   where info.WorkId == workerId
                                   select info.ApplicationId).ToArray();
            //再找出申请列表
            var applications = (from apply in _ctx.Apply
                               join worker in _ctx.Worker on apply.WorkerId equals worker.Id
                               join deparment in _ctx.Deparment on apply.DeparmentId equals deparment.Id
                               where applicationIds.Contains(apply.Id) && worker.Name.Contains(checkingDto.Query)
                               select new
                               {
                                   id = apply.Id,
                                   workerName = worker.Name,
                                   deparment = deparment.Name,
                                   type = _approvalService.GetApplicationType(apply.Type1, apply.Type2),
                                   state = apply.State,
                                   stateName = _approvalService.GetStateName(apply.State),
                                   startTime = apply.StartTime,
                                   endTime = apply.EndTime,
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
        public object CheckApplication(CheckDto checkDto, string account)
        {
            var result = new object();
            try
            {
                //找出处理人的ID
                Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
                //找出申请
                Apply apply = _ctx.Apply.Find(checkDto.ApplicationId);
                string remark = worker.Name + "：" + checkDto.Remark + ";" + apply.Remark;
                //更新申请的处理信息
                apply.HandleTime = DateTime.Now.ToFileTime();
                apply.LeaderId = worker.Id;
                apply.Remark = remark;
                apply.State = checkDto.IsAgree ? 2 : 3;
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


    }
}
