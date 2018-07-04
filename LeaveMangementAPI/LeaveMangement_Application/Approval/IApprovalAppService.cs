using LeaveMangement_Entity.Dtos.Approval;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Approval
{
    public interface IApprovalAppService
    {
        object GetInform(string account);
        object AddApplication(AddApplicationDto addApplicationDto);
        object GetApplicationList(GetApplicationListDto getApplicationListDto);
        object GetUnApplicationList(GetApplicationListDto getApplicationListDto);
        object GetApplicationById(int id);
        object SubmitApplication(int id);
        object EditApplication(EditApplicationDto editApplicationDto);
        object DeleteApplicationById(int id);
        int GetApprovalCount(string account, int compId);
        object GetCheckingList(CheckingDto checkingDto);
        object CheckApplication(CheckDto checkDto,string account);
    }
}
