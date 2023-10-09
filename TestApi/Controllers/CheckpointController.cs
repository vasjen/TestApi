using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckpointController : ControllerBase
    {
        public CheckpointController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }
        
        private readonly ILogger<CheckpointController> _logger;
        private readonly IShiftService _shiftService;

        public CheckpointController(ILogger<CheckpointController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> StartShift(Dto.ShiftRequest request)
        {
            if (!await _shiftService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            if (await _shiftService.EmployeeIsWorking(request.EmployeeId, request.Time))
                return BadRequest($"Employee with ID {request.EmployeeId} is already working");
            
            await _shiftService.StartWork(request.EmployeeId, request.Time);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> EndShift(Dto.ShiftRequest request)
        {
            if (!await _shiftService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            if (!await _shiftService.EmployeeIsWorking(request.EmployeeId, request.Time))
                return BadRequest($"Employee with ID {request.EmployeeId} is not working");
            
            await _shiftService.EndWork(request.EmployeeId, request.Time);
            return Ok();
        }
    }
}