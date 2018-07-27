using LeaveMangement_Entity.Dtos.Notices.NoticeType;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Core.Notices.NoticeType
{
    public class TypeProvider
    {
        public static readonly List<Types> _types = new List<Types>
        {
            new Types {Code = 1,TypeName="公司公告" },
            new Types {Code = 2,TypeName="部门公告" },
        };
    }
}
