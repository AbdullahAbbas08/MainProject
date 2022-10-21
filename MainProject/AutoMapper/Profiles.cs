using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DataAccessLayer.Models.Task;

namespace BussinessLayer.AutoMapper
{
    public class Profiles:Profile
    {
        public Profiles()
        {
            CreateMap<Employee, EmployeeDataDto>();
            CreateMap<EmployeeDataDto,Employee>();
            CreateMap<Insert_Update_EmployeeDto, Employee>();
            CreateMap<Employee,Insert_Update_EmployeeDto>();
            
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}
