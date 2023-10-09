using System.Collections.Generic;

namespace TestApi.Models
{
    public class Employee
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public Position Position { get; set; }
        public List<Work> Works { get; } = new List<Work>();

    }
}