using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Worker
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public int? Sex { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int? PaperType { get; set; }
        public string PaperNumber { get; set; }
        public DateTime? Brith { get; set; }
        public int? Age { get; set; }
        public bool IsAuth { get; set; }
        public int State { get; set; }
    }
}
