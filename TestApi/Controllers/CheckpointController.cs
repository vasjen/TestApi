using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckpointController : ControllerBase
    {
        private readonly ILogger<CheckpointController> _logger;
        private readonly IShiftService _shiftService;

        public CheckpointController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StartShift(ShiftRequest request)
        {
            if (!await _shiftService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            if (await _shiftService.EmployeeIsWorking(request.EmployeeId, request.Time))
                return BadRequest($"Employee with ID {request.EmployeeId} is already working");
            
            await _shiftService.StartWork(request.EmployeeId, request.Time);
            return Ok();
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EndShift(ShiftRequest request)
        {
            if (!await _shiftService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            if (!await _shiftService.EmployeeIsWorking(request.EmployeeId, request.Time))
                return BadRequest($"Employee with ID {request.EmployeeId} is not working");
            
           var work = await _shiftService.EndWork(request.EmployeeId, request.Time);
            return Ok(work);
        }
    }
}