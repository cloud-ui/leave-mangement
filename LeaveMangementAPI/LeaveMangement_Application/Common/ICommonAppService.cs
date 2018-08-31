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
    }
}
