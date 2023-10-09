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
    public class CheckpointController : ControllerBase
    {
        
        private readonly ILogger<CheckpointController> _logger;

        public CheckpointController(ILogger<CheckpointController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return new List<Employee>();
        }
    }
}