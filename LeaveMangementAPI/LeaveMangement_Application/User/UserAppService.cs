using LeaveMangement_Core.User;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.User
{
    public class UserAppService : IUserAppService
    {
        private UserManager _userManager = new UserManager();
        public Worker Login(string account, string password)
        {
            return _userManager.Login(account,password);
        }
    }
}
