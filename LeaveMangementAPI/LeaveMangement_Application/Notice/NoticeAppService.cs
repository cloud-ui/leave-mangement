using System;
using System.Collections.Generic;
using System.Text;
using LeaveMangement_Core.Notices;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Entity.Dtos.Notices;

namespace LeaveMangement_Application.Notice
{
    public class NoticeAppService : INoticeAppService
    {
        private readonly NoticeManager _noticeManager = new NoticeManager();
        public Result AddNotice(AddNotice addNotice, string account)
        {
            return _noticeManager.AddNotice(addNotice,account);
        }
        public Result DeleteNotice(int id)
        {
            return _noticeManager.DeleteNotice(id);
        }

        public object NoticeList(QueryList queryList, string account)
        {
            return _noticeManager.NoticeList(queryList, account);
        }

        public object GetNoticeList(string account, int compId)
        {
            return _noticeManager.GetNoticeList(account, compId);
        }
    }
}
