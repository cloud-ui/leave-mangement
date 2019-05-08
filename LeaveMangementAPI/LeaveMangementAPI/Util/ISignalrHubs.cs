using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveMangementAPI.Authorization
{
    public interface ISignalrHubs
    {
        Task Send(string id, string message);
    }
}
