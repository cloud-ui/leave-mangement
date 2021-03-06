﻿using LeaveMangement_Core.Common;
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
        public int GetDepId(string depName,int compId)
        {
            return _commonManager.GetDepId(depName,compId);
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
        public int GetPosition(string positionName, int compId)
        {
            return _commonManager.GetPosition(positionName, compId);
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

        public string GetCompName(int compId)
        {
            return _commonManager.GetCompName(compId);
        }

        public int GetLeaveCount(string account, int compid)
        {
            return _commonManager.GetLeaveCount(account, compid);
        }

        public bool JudgeAuth(string account, string path)
        {
            return _commonManager.JudgeAuth(account, path);
        }

        public string GetUserAccount(int id)
        {
            return _commonManager.GetUserAccount(id);
        }
        public int GetManagerPosition(int compId)
        {
            return _commonManager.GetManagerPosition(compId);
        }
        public int GetManagerState(int compId)
        {
            return _commonManager.GetManagerState(compId);
        }
    }
}
