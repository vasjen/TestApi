using System;

namespace TestApi.Models
{
    public class Work
    {
        public int Id { get; init; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Hours { get; set; }
        public int EmployeeId { get; init; }
        public Employee Employee { get; set; } = null!;
    }
}