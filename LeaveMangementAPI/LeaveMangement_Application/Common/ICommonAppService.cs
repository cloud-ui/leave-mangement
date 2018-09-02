using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Common
{
    public interface ICommonAppService
    {
        int GetUserCompId(string account);
        int GetCompId(string componentName);
        int GetUserDepId(string account);
        int GetUserId(string userName);
        bool IsExitDep(string depName, int companyId);
        bool IsExitWorker(int? paperType, string paperNumber, int compId);
        int GetDepId(string depName);
        int GetPaperType(string paperType);
        int GetState(string name, int compId);
        void ChangeDepWorkerCount(List<Worker> workers);
    }
}
