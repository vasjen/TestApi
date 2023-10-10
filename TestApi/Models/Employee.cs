#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class Employee
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Surname is required")]
        public string SurName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        [Required(ErrorMessage = "Position is required")]
        public Position Position { get; set; }
        public List<Work> Works { get; } = new List<Work>();

    }
}