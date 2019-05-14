using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Approval;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Approval
{
    public interface IApprovalAppService
    {
        List<Inform> GetInform(string account);
        Result AddApplication(AddApplicationDto addApplicationDto);
        object GetApplicationList(GetApplicationListDto getApplicationListDto);
        object GetUnApplicationList(GetApplicationListDto getApplicationListDto);
        object GetApplicationById(int id);
        Result SubmitApplication(int id);
        object EditApplication(EditApplicationDto editApplicationDto);
        object DeleteApplicationById(int id);
        int GetApprovalCount(string account, int compId);
        object GetCheckingList(CheckingDto checkingDto);
        object GetApplyJobList(CheckingDto checkingDto);
        object CheckApplication(CheckDto checkDto,string account);
        Result PushCheck(PushCheck pushCheck,string account);
        object RevokeApplication(int id);
        Result CreateApplyOfJob(ApplyJobDto applyJobDto,string account);
    }
}
