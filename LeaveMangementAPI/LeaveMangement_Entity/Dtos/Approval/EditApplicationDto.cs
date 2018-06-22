using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Approval
{
    public class EditApplicationDto
    {
        public int Id { get; set; }
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        //请假理由
        public string Account { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsSubmit { get; set; }
    }
}
