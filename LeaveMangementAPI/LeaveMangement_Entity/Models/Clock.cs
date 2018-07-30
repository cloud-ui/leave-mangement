using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Clock
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public string ClockDay { get; set; }
        public long SrartTime { get; set; }
        public long? EndTime { get; set; }
        public bool IsFull { get; set; }
        public double WorkHour { get; set; }
    }
}
