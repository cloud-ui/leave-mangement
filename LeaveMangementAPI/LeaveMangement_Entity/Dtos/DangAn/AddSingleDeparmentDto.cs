using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class AddSingleDeparmentDto
    {
        public string Name { get; set; }
        public int CompId { get; set; }
        public int MangerId { get; set; }
        public int WorkerCount { get; set; }
        public int Id { get; set; }
    }
}
