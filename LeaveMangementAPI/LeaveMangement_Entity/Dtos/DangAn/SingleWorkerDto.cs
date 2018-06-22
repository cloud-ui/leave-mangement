using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class SingleWorkerDto
    {
        public string Name { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        //0:女 1：男
        public int Sex { get; set; }
        //证件类型
        public int PaperType { get; set; }
        //证件号码
        public string PaperNumber { get; set; }
        //入职时间
        public DateTime? EntryTime { get; set; }
        //入职状态
        public int State { get; set; }
    }
}
