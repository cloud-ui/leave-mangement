using LeaveMangement_Core.Common;
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
        public int GetUserDepId(string account)
        {
            return _commonManager.GetUserDepId(account);
        }
    }
}
