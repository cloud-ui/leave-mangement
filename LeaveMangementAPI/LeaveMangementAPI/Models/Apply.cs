using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Apply
    {
        public int Id { get; set; }
        public int WorkerId { get; set; }
        public int DepartmentId { get; set; }
        public int PositionId { get; set; }
        public int Type1 { get; set; }
        public int Type2 { get; set; }
        public string Account { get; set; }
        public int? State { get; set; }
        public string Remark { get; set; }
        public int? LeaderId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? HandleTime { get; set; }
        public bool IsSubmit { get; set; }
    }
}
