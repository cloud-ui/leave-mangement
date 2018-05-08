using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Models
{
    public partial class Clock
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public DateTime ClockDay { get; set; }
        public DateTime? SrartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsFull { get; set; }
    }
}
