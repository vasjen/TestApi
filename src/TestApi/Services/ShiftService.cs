using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Services
{
    public class ShiftService : IShiftService
    {
        private readonly AppDbContext _context;

        public ShiftService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> EmployeeIsExist(int employeeId)
        {
            return await _context.Employees.AnyAsync(p => p.Id == employeeId);
        }

        public async Task<bool> EmployeeIsWorking(int employeeId, DateTime time)
        {
            var employee = await _context.Employees
                .Include(p => p.Works)
                .AsNoTracking()
                .SingleAsync(p => p.Id == employeeId);
            var work = employee.Works.LastOrDefault();
            if (work == null)
                return false;

            if (work.Start <= time)
            {
                if (work.End == default)
                    return true;
                else
                    return false;

            }
            

           

            return false;
        }

        public async Task StartWork(int employeeId, DateTime time)
        {
            var work = new Work
            {
                Start = time,
                EmployeeId = employeeId
            };
            await _context.Works.AddAsync(work);
            await _context.SaveChangesAsync();
            
        }

        public async Task<ShiftResponse> EndWork(int employeeId, DateTime time)
        {
            var work = await _context.Works
               .OrderBy(p => p.Id)
               .LastOrDefaultAsync(p => p.EmployeeId == employeeId);
            work.End = time;
            work.Hours = (int) (work.End - work.Start).TotalHours;
            await _context.SaveChangesAsync();
            
            return new ShiftResponse(employeeId, work.Start, work.End, work.Hours);
            
        }
    }
}