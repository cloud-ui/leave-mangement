using LeaveMangement_Core.Common;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Common
{
    public class CommonAppService:ICommonAppService
    {
        private CommonManager _commonManager = new CommonManager();
        public int GetUserCompId(string account)
        {
            return _commonManager.GetUserCompId(account);
        }
        public int GetCompId(string componentName)
        {
            return _commonManager.GetCompId(componentName);
        }
        public int GetDepId(string depName)
        {
            return _commonManager.GetDepId(depName);
        }
        public int GetUserDepId(string account)
        {
            return _commonManager.GetUserDepId(account);
        }
        public int GetUserId(string userName)
        {
            return _commonManager.GetUserId(userName);
        }
        public int GetPaperType(string paperType)
        {
            return _commonManager.GetPaperType(paperType);
        }
        public int GetState(string name, int compId)
        {
            return _commonManager.GetState(name, compId);
        }
        public void ChangeDepWorkerCount(List<Worker> workers)
        {
            _commonManager.ChangeDepWorkerCount(workers);
        }
        public bool IsExitDep(string depName, int companyId)
        {
            return _commonManager.IsExitDep(depName, companyId);
        }
        public bool IsExitWorker(int? paperType, string paperNumber, int compId)
        {
            return _commonManager.IsExitWorker(paperType, paperNumber, compId);
        }
    }
}
