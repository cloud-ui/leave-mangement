using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Position
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int DeparmenyId { get; set; }
        public string Name { get; set; }
    }
}
