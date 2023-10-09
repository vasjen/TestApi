using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HRDepartmentController : ControllerBase
    {
        
        private readonly ILogger<HRDepartmentController> _logger;

        public HRDepartmentController(ILogger<HRDepartmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> StartShift()
        {
            var employ
            return Ok();
        }
    }
}