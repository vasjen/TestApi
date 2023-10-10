using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HRDepartmentController : ControllerBase
    {
        
        private readonly ILogger<HRDepartmentController> _logger;
        private readonly IHRService _hrService;

        public HRDepartmentController(ILogger<HRDepartmentController> logger, IHRService hrService)
        {
            _logger = logger;
            _hrService = hrService;
        }
        
        [HttpGet("employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees([FromQuery]Position? position)
        {
            if (position != null)
            {
                if (Enum.IsDefined(typeof(Position), position.Value))
                    return Ok(await _hrService.GetEmployees(position.Value));
                else
                    return BadRequest($"Position {position} is not exist");
            }
            
            return Ok(await _hrService.GetEmployees());
        }
        
        [HttpGet("positions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public PositionResponse GetPositions()
        {   
            return _hrService.GetPositions();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateRequest request)
        {
            if (!_hrService.PositionIsExist(request.Position))
                return BadRequest($"Position {request.Position} is not exist");
            
            
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.SurName))
                return BadRequest("One or more fields is null");
            
            var employee = await _hrService.CreateEmployee(request);
            return Created("",employee);
        }
        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateRequest request)
        {
            if (!await _hrService.EmployeeIsExist(request.EmployeeId))
                return NotFound($"Employee with ID {request.EmployeeId} is not exist");
            
            if (request.Name is null || request.MiddleName is null || request.Position == null)
                return BadRequest("One or more fields is null");
            
            var employee = await _hrService.UpdateEmployee(request);
            return Ok(employee);
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(EmployeeDeleteRequest request)
        {
            if (!await _hrService.EmployeeIsExist(request.EmployeeId))
                return NotFound($"Employee with ID {request.EmployeeId} is not exist");
            
            await _hrService.DeleteEmployee(request);
            return Ok();
        }
        
        
    }
}