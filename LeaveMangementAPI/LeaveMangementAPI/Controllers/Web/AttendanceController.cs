using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMangementAPI.Controllers.Web
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("any")]
    public class AttendanceController : Controller
    {
    }
}