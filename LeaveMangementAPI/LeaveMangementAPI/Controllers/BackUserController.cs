using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMangementAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/BackUser/Action")]
    public class BackUserController : Controller
    {
        [HttpPost]
        public void Login(string account,string password)
        {
        }
    }
}