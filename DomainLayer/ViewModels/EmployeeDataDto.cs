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

    }
}
