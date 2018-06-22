using System;
using System.Collections.Generic;
using System.Text;
using LeaveMangement_Core.Approval;
using LeaveMangement_Entity.Dtos.Approval;

namespace LeaveMangement_Application.Approval
{
    public class ApprovalAppService : IApprovalAppService
    {
        private ApprovalManager _approvalManager = new ApprovalManager();
        public object AddApplication(AddApplicationDto addApplicationDto)
        {
            return _approvalManager.AddApplication(addApplicationDto);
        }
        public object GetApplicationList(string account)
        {
            return _approvalManager.GetApplicationList(account);
        }
        public object GetUnApplicationList(string account)
        {
            return _approvalManager.GetUnApplicationList(account);
        }
        public object GetApplicationById(int id)
        {
            return _approvalManager.GetApplicationById(id);
        }
        public object SubmitApplication(int id)
        {
            return _approvalManager.SubmitApplication(id);
        }
        public object EditApplication(EditApplicationDto editApplicationDto)
        {
            return _approvalManager.EditApplication(editApplicationDto);
        }
    }
}
