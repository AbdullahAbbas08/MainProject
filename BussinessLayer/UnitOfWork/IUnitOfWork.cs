using BussinessLayer.Repository;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Hosting;
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
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataBaseContext DataBaseContext;
        private readonly string webRootPath;
        public UnitOfWork(DataBaseContext _DataBaseContext, IHostingEnvironment hostEnvironment) 
        { DataBaseContext = _DataBaseContext;
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


    }

  

}