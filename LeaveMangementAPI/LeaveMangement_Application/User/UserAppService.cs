using LeaveMangement_Core.User;
using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.User
{
    public class UserAppService : IUserAppService
    {
        private UserManager _userManager = new UserManager();
        public List<Menus> GetMenu(int positionId, int parentId = 0)
        {
            return _userManager.GetMenu(parentId,positionId);
        }
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
        public object ModifyPassword(ModifyPasswordDto modifyPasswordDto)
        {
            return _userManager.ModifyPassword(modifyPasswordDto);
        }
        public object EditUserMessage(EditUserMessageDto editUserMessageDto)
        {
            return _userManager.EditUserMessage(editUserMessageDto);
        }
    }
}
