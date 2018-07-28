using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveMangement_Entity.Dtos.DangAn
{
    public class TransferWorkerDto
    {
        public int DeparmentId { get; set; }
        public int StateId { get; set; }
        public int PositionId { get; set; }
        public int WorkerId { get; set; }
    }
}
