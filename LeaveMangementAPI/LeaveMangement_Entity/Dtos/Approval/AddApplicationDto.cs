using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Approval
{
    public class AddApplicationDto
    {
        public int WorkerId { get; set; }
        public int CompId { get; set; }
        public int DeparmentId { get; set; }
        //事前事后
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        //请假理由
        public string Account { get; set; }
        public long StartTime { get; set; }
        public long EndTime { get; set; }
        public bool IsSubmit { get; set; }
    }
}
