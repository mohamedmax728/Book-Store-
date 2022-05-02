using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private LogsService _LogsService;
        public LogsController(LogsService LogsService)
        {
            _LogsService = LogsService;
        }
        [HttpGet("get-all-logs-from-db")]
        public IActionResult GetAllLogsFromDB()
        {
            try
            {
                return Ok(_LogsService.GetAllLogsFromDB());
            }
            catch
            {
                return BadRequest("couldnot load any log from database");
            }
        }
    }
}
