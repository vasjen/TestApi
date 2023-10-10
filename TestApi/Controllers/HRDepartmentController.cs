using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApi.Models;
using TestApi.Services;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HrDepartmentController : ControllerBase
    {
        
        private readonly IHRService _hrService;

        public HrDepartmentController(IHRService hrService)
        {
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
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateRequest request)
        {
            if (!await _hrService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            if (!_hrService.PositionIsExist(request.Position))
                return BadRequest($"Position {request.Position} is not exist");
            
            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.SurName))
                return BadRequest("One or more fields is null");
            
            var employee = await _hrService.UpdateEmployee(request);
            return Ok(employee);
        }
        
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(BadRequestResult))]
        public async Task<IActionResult> DeleteEmployee(EmployeeDeleteRequest request)
        {
            if (!await _hrService.EmployeeIsExist(request.EmployeeId))
                return BadRequest($"Employee with ID {request.EmployeeId} is not exist");
            
            await _hrService.DeleteEmployee(request);
            return Ok();
        }
        
        
    }
}