using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.User
{
    public interface IUserAppService
    {
        object Login(string account,string password);
    }
}
