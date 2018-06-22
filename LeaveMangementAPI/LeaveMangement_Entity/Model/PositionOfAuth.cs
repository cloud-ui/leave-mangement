using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class PositionOfAuth
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int AuthId { get; set; }
    }
}
