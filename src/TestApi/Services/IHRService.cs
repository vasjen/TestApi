using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestApi.Models;

namespace TestApi.Services
{
    public interface IHRService
    {
        Task<EmployeeResponse> CreateEmployee(EmployeeCreateRequest request);
        Task<EmployeeResponse> UpdateEmployee(EmployeeUpdateRequest request);
        Task DeleteEmployee(EmployeeDeleteRequest request);
        Task<IEnumerable<EmployeeResponse>> GetEmployees();
        Task<IEnumerable<EmployeeResponse>> GetEmployees(Position position);
        PositionResponse  GetPositions();
        
        Task<bool> EmployeeIsExist(int employeeId);
        bool PositionIsExist(string position);
    }
}