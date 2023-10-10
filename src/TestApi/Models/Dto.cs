#nullable enable
using System;
using System.Collections.Generic;
using TestApi.Models;

namespace TestApi
{
    
        public record ShiftRequest(int EmployeeId, DateTime Time);
        public record ShiftResponse(int EmployeeId, DateTime Start, DateTime End, int Hours);
        
        public record EmployeeCreateRequest(string Name, string SurName, string Position, string? MiddleName = null);
        public record EmployeeUpdateRequest(int EmployeeId,string Name, string SurName, string Position, string? MiddleName = null);
        public record EmployeeDeleteRequest(int EmployeeId);
        public record EmployeeResponse(int Id, string Name, string SurName, string? MiddleName, string Position);
        public record PositionResponse(List<string> Positions);
    
}