using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Dtos.User;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.User
{
    public interface IUserAppService
    {
        Worker Login(string account,string password);
        object AddSingleWorker(SingleWorkerDto singleWorkerDto);
        object GetWorkerById(int userId);
        object ModifyPassword(ModifyPasswordDto modifyPasswordDto);
        List<Menus> GetMenu(int positionId, int parentId = 0);
        object EditUserMessage(EditUserMessageDto editUserMessageDto);
    }
}
