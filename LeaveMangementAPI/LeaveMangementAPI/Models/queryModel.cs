using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Models
{
    public class QueryModel
    {
        public int CurrentPage { get; set; }
        public int CurentPageSize { get; set; }
        public string Query { get; set; }
        public int QueryId { get; set; }
    }
}
