using System;
using System.Collections.Generic;

namespace LeaveMangement_Entity.Model
{
    public partial class Image
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public int OwnerId { get; set; }
        public string OwnerType { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
