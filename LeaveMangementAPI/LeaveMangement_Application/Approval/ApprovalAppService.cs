﻿using System;
using System.Collections.Generic;
using System.Text;
using LeaveMangement_Core.Approval;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangement_Entity.Models;

namespace LeaveMangement_Application.Approval
{
    public class ApprovalAppService : IApprovalAppService
    {
        private ApprovalManager _approvalManager = new ApprovalManager();
        public List<Inform> GetInform(string account)
        {
            return _approvalManager.GetInform(account);
        }
        public Result AddApplication(AddApplicationDto addApplicationDto)
        {
            return _approvalManager.AddApplication(addApplicationDto);
        }
        public object GetApplicationList(GetApplicationListDto getApplicationListDto)
        {
            return _approvalManager.GetApplicationList(getApplicationListDto);
        }
        public object GetUnApplicationList(GetApplicationListDto getApplicationListDto)
        {
            return _approvalManager.GetUnApplicationList(getApplicationListDto);
        }
        public object GetApplicationById(int id)
        {
            return _approvalManager.GetApplicationById(id);
        }
        public Result SubmitApplication(int id)
        {
            return _approvalManager.SubmitApplication(id);
        }
        public object EditApplication(EditApplicationDto editApplicationDto)
        {
            return _approvalManager.EditApplication(editApplicationDto);
        }
        public object DeleteApplicationById(int id)
        {
            return _approvalManager.DeleteApplicationById(id);
        }
        public int GetApprovalCount(string account, int compId)
        {
            return _approvalManager.GetApprovalCount(account, compId);
        }
        public object GetCheckingList(CheckingDto checkingDto)
        {
            return _approvalManager.GetCheckingList(checkingDto);
        }
        public object GetApplyJobList(CheckingDto checkingDto)
        {
            return _approvalManager.GetApplyJobList(checkingDto);
        }
        public object CheckApplication(CheckDto checkDto, string account)
        {
            return _approvalManager.CheckApplication(checkDto, account);
        }
        public Result PushCheck(PushCheck pushCheck, string account)
        {
            return _approvalManager.PushCheck(pushCheck, account);
        }
        public object RevokeApplication(int id)
        {
            return _approvalManager.RevokeApplication(id);
        }
        public Result CreateApplyOfJob(ApplyJobDto applyJobDto, string account)
        {
            return _approvalManager.CreateApplyOfJob(applyJobDto, account);
        }
    }
}
