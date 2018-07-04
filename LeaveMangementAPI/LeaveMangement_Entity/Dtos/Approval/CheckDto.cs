using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Approval
{
    public class CheckDto
    {
        public string Remark { get; set; }
        public int ApplicationId { get; set; }
        public bool IsAgree { get; set; }
    }
}
