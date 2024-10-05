﻿using System.ComponentModel.DataAnnotations;

namespace CoffeeBrowser.HRM.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public List<Employee> Employees { get; set; } = [];
    }
}
