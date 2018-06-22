using LeaveMangement_Entity.Dtos.Approval;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Approval
{
    public interface IApprovalAppService
    {
        object AddApplication(AddApplicationDto addApplicationDto);
        object GetApplicationList(string account);
        object GetUnApplicationList(string account);
        object GetApplicationById(int id);
        object SubmitApplication(int id);
        object EditApplication(EditApplicationDto editApplicationDto);
    }
}
