using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class PositionOfAuth
    {
        public int Id { get; set; }
        public int PositionId { get; set; }
        public int AuthId { get; set; }
    }
}
