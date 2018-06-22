using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CellphoneNumber { get; set; }
        public string Corporation { get; set; }
        public string Email { get; set; }
        public int DeparmentCount { get; set; }
        public int WokerCount { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
