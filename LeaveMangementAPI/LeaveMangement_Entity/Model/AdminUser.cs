using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class AdminUser
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int? CompanyId { get; set; }
    }
}
