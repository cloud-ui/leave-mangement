using LeaveMangement_Core.User;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Model;
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
        public object AddSingleWorker(SingleWorkerDto singleWorkerDto)
        {
            return _userManager.AddSingleWorker(singleWorkerDto);
        }
        public object GetWorkerById(int userId)
        {
            return _userManager.GetWorkerById(userId);
        }
    }
}
