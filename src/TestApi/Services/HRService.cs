using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Services
{
    public class HRService : IHRService
    {
        private readonly AppDbContext _context;

        public HRService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeResponse> CreateEmployee(EmployeeCreateRequest request)
        {
            Console.WriteLine("Request position: {0}",request.Position);
            var employee = new Employee()
            {
                Name = request.Name,
                SurName = request.SurName,
                Position = Enum.Parse<Position>(request.Position),
                MiddleName = request.MiddleName ?? String.Empty
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return new EmployeeResponse(
                Id: employee.Id, 
                Name: employee.Name, 
                SurName: employee.SurName, 
                MiddleName: employee.MiddleName, 
                Position: employee.Position.ToString()
                );
        }

        public async Task<EmployeeResponse> UpdateEmployee(EmployeeUpdateRequest request)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            employee.Name = request.Name;
            employee.SurName = request.SurName;
            employee.Position = Enum.Parse<Position>(request.Position);
            employee.MiddleName = request.MiddleName ?? String.Empty;
            
            await _context.SaveChangesAsync();
            
            return new EmployeeResponse(
                Id: employee.Id, 
                Name: employee.Name, 
                SurName: employee.SurName, 
                MiddleName: employee.MiddleName, 
                Position: employee.Position.ToString()
            );
        }

        public async Task DeleteEmployee(EmployeeDeleteRequest request)
        {
            var employee = await _context.Employees.FindAsync(request.EmployeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeResponse>> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees.Select(p => new EmployeeResponse(p.Id, p.Name, p.SurName, p.MiddleName, p.Position.ToString()));
        }
        public async Task<IEnumerable<EmployeeResponse>> GetEmployees(Position position)
        {
            var employees = await _context.Employees.Where(p => p.Position == position).ToListAsync();
            return employees.Select(p => new EmployeeResponse(p.Id, p.Name, p.SurName, p.MiddleName, p.Position.ToString()));
        }

        public PositionResponse GetPositions()
        {
            var positions = Enum.GetValues(typeof(Position)).Cast<Position>().ToList();
            return new PositionResponse(positions.Select(p => p.ToString()).ToList());
        }
    

        public async Task<bool> EmployeeIsExist(int employeeId)
        {
            return await _context.Employees.AnyAsync(p => p.Id == employeeId);
        }

        public bool PositionIsExist(string position)
        {
            return Enum.IsDefined(typeof(Position), position);
        }
    }
}