using LeaveMangement_Entity.Dtos.Notices;
using LeaveMangement_Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LeaveMangement_Entity.Dtos;
using LeaveMangement_Core.Common;
using LeaveMangement_Core.Notices.NoticeType;

namespace LeaveMangement_Core.Notices
{
    public class NoticeManager
    {
        private KaoQinContext _ctx = new KaoQinContext();
        private CommonServer _commonServer = new CommonServer();
        public Result AddNotice(AddNotice addNotice, string account)
        {
            Result result = new Result();
            int workerId = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account)).Id;
            Notice newNotice = new Notice()
            {
                Title = addNotice.Title,
                Content = addNotice.Content,
                Type = addNotice.Type,
                CreateTime = DateTime.Now.ToFileTime(),
                IsDelete = false,
                CreateHuman = workerId
            };
            try
            {
                _ctx.Notice.Add(newNotice);
                _ctx.SaveChanges();
                result.IsSuccess = true;
                result.Message = "添加公告成功！";
            }catch(Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }
        public Result DeleteNotice(int id)
        {
            Result result = new Result();
            Notice notice = _ctx.Notice.Find(id);
            try
            {
                notice.IsDelete = true;
                _ctx.SaveChanges();
                result.IsSuccess = true;
                result.Message = "删除公告成功！";
            }
            catch(Exception e)
            {
                result.IsSuccess = false;
                result.Message = e.Message;
            }
            return result;
        }
        public object NoticeList(QueryList queryList, string account)
        {
            queryList.Query = string.IsNullOrEmpty(queryList.Query) ? "" : queryList.Query;
            Worker worker = _ctx.Worker.SingleOrDefault(w => w.Account.Equals(account));
            var noticeList = (from notice in _ctx.Notice
                              join w in _ctx.Worker on notice.CreateHuman equals w.Id
                              where notice.IsDelete == false && notice.Title.Contains(queryList.Query)
                              orderby notice.Id
                              select new
                              {
                                  id = notice.Id,
                                  title = notice.Title,
                                  content = notice.Content,
                                  createHuman = w.Name,
                                  typeCode = notice.Type,
                                  type = TypeProvider._types.SingleOrDefault(t=>t.Code==notice.Type).TypeName,
                                  createTime = _commonServer.ChangeTime(notice.CreateTime)
                              }).ToList();
            var result = new
            {
                totalCount = noticeList.Count(),
                data = noticeList.Skip((queryList.CurrentPage - 1) * queryList.CurrentPageSize).Take(queryList.CurrentPageSize),
            };
            return result;
        }
    }
}
