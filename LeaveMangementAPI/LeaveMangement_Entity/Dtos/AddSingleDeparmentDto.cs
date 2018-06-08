using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos
{
    public class AddSingleDeparmentDto
    {
        public string Name { get; set; }
        public int CompId { get; set; }
        public int? MangerId { get; set; }
    }
}
