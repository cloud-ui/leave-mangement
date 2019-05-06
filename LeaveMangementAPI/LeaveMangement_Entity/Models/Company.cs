using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public double Lng { get; set; }
        public double Lat { get; set; }

        public string CellphoneNumber { get; set; }
        public string Corporation { get; set; }
        public string Email { get; set; }
        public int DeparmentCount { get; set; }
        public int WokerCount { get; set; }
        public long CreateTime { get; set; }
    }
}
