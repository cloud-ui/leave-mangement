using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int? AuthId { get; set; }
    }
}
