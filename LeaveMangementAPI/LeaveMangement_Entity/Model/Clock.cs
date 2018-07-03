using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Clock
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public long ClockDay { get; set; }
        public long? SrartTime { get; set; }
        public long? EndTime { get; set; }
        public bool IsFull { get; set; }
    }
}
