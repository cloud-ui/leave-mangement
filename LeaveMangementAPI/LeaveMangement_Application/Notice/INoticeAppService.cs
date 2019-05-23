using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Notices;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Application.Notice
{
    public interface INoticeAppService
    {
        Result AddNotice(AddNotice addNotice,string account);
        Result DeleteNotice(int id);
        object NoticeList(QueryList queryList,string account);

        object GetNoticeList(string account, int compId);
    }
}
