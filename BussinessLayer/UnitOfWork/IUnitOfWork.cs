using AutoMapper;
using BussinessLayer.Helpers;
using BussinessLayer.Repository;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = DataAccessLayer.Models.Task;

namespace BussinessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDepartmentRepository Departments { get; }
        IEmployeeRepository Employees { get; }
        ITasksRepository Tasks { get; }
        int SaveChanges();
        string WebRootPath { get; }
        IMapper Mapper { get; }
        Helper helper { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext DataBaseContext;
        private readonly IMapper mapper;
        private readonly Helper __helper;
        private readonly string webRootPath;
        public UnitOfWork(DataBaseContext _DataBaseContext, 
                            IHostingEnvironment hostEnvironment, 
                            IMapper _mapper,
                            IOptions<Helper> _helper) 
        { DataBaseContext = _DataBaseContext;
            mapper = _mapper;
            __helper = _helper.Value;
            this.webRootPath = hostEnvironment.WebRootPath;
        }


        private IDepartmentRepository departments; 
        public IDepartmentRepository Departments 
        { get 
            { 
                if (departments == null)
                { 
                    departments = new DepartmentRepository(DataBaseContext, this);
                }
                return departments;
            }
        }
        
        private IEmployeeRepository employees;  
        public IEmployeeRepository Employees
        { get 
            { 
                if (employees == null)
                {
                    employees = new EmployeeRepository(DataBaseContext, this);
                }
                return employees;
            }
        }
        
        private ITasksRepository tasks;  
        public ITasksRepository Tasks 
        { get 
            { 
                if (tasks == null)
                {
                    tasks = new TasksRepository(DataBaseContext, this);
                }
                return tasks;
            }
        }

        public int SaveChanges()
        {
            return DataBaseContext.SaveChanges();
        }

        public string WebRootPath => this.webRootPath;
        public IMapper Mapper => this.mapper;
        public Helper helper => __helper;


    }

  

}