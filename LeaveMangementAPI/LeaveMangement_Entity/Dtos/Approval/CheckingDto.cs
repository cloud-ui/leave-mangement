﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.Approval
{
    public class CheckingDto
    {
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string Query { get; set; }
        public string Account { get; set; }
        public int CompId { get; set; }
    }
}
