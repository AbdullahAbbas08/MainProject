using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using Task = DataAccessLayer.Models.Task;

namespace DomainLayer.ViewModels
{
    public class EmployeeDataDto
    {
        public string Id { get; set; }   
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ImagePath { get; set; }
        public decimal Salay { get; set; }
        public int? DepartmentId { get; set; }
        public string? ManagerId { get; set; }
        public string ManagerFullName { get; set; }   
        public  List<Task> Tasks { get; set; }

    }
    
    public class Insert_Update_EmployeeDto 
    {
        public string Id { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public decimal Salay { get; set; }
        public string ImagePath { get; set; }
        public IFormFile Image { get; set; }
        public int? DepartmentId { get; set; }
        public string? ManagerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

    }
    
    public class TaskDto 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EmployeeCount { get; set; }
        public decimal SumSalary { get; set; }
    }
}
