using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Logging.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoggingController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Log> Get()
        {
            return new List<Log>
            {
                new Log
                {
                    Date = DateTime.UtcNow,
                    Content = "Dummy logging"
                }
            };
        }
    }
}