using LeaveMangement_Entity.Dtos.DangAn;
using LeaveMangement_Entity.Model;
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
    }
}
