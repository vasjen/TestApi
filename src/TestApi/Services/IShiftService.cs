using System;
using System.Threading.Tasks;

namespace TestApi.Services
{
    public interface IShiftService
    {
        Task<bool> EmployeeIsExist(int employeeId);
        
        Task<bool> EmployeeIsWorking(int employeeId, DateTime time);
        
        Task StartWork(int employeeId, DateTime time);
        Task<ShiftResponse> EndWork(int employeeId, DateTime time);
    }
}