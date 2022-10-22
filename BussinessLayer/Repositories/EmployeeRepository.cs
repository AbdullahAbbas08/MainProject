using BussinessLayer.Constants;
using BussinessLayer.UnitOfWork;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using DomainLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BussinessLayer.Repository
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        List<SelectListItem> GetManagersSelectItems();
        List<SelectListItem> GetEmployeeSelectItems(); 
        List<SelectListItem> GetDepartmentSelectItems();
        CreateEmployeesDropdowns InitViewModel(); 
        CreateEmployeesDropdowns Edit(string? ID);
        Insert_Update_EmployeeDto Edit(CreateEmployeesDropdowns model);
        System.Threading.Tasks.Task CreateAsync(CreateEmployeesDropdowns model);

        void Delete(string id);

        Employee GetEmployee(string id); 
        void ChangeTaskStatus(int taskid, TaskState status );
        public void AssignTask(string empid, int taskid);
    }

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly DataBaseContext dbContext;
        private readonly IUnitOfWork uow;

        public EmployeeRepository(DataBaseContext _DbContext ,IUnitOfWork uow) : base(_DbContext, uow)
        {
            dbContext = _DbContext;
            this.uow = uow;
        }

        public List<SelectListItem> GetManagersSelectItems()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            var ManagerRoleID  = dbContext.Roles.FirstOrDefault(x=>x.Name == Roles.Manager)?.Id;
            var UserIdsList = dbContext.UserRoles.Where(x=>x.RoleId == ManagerRoleID).Select(x=>x.UserId).ToList();
            var query = dbContext.Users
                                 .Where(x => UserIdsList
                                 .Contains(x.Id))
                                 .Select(x=>new SelectItemDto<string>{ Text=x.FullName,value=x.Id})
                                 .ToList();

            listItems.Add(new SelectListItem
            {
                Text = "No Manager",
                Value ="0"
            });

            foreach (var item in query)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.value
                });
            }
            return listItems;
        } 
        
        public List<SelectListItem> GetEmployeeSelectItems() 
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            var RoleID  = dbContext.Roles.FirstOrDefault(x=>x.Name == Roles.Employee)?.Id;
            var UserIdsList = dbContext.UserRoles.Where(x=>x.RoleId == RoleID).Select(x=>x.UserId).ToList();
            var query = dbContext.Users
                                 .Where(x => UserIdsList
                                 .Contains(x.Id))
                                 .Select(x=>new SelectItemDto<string>{ Text=x.FullName,value=x.Id})
                                 .ToList();

            listItems.Add(new SelectListItem
            {
                Text = "None",
                Value ="0"
            });

            foreach (var item in query)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.value
                });
            }
            return listItems;
        }  
        public List<SelectListItem> GetDepartmentSelectItems() 
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            var Department  = dbContext.Departments.ToList();

            foreach (var item in Department)
            {
                listItems.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                });
            }
            return listItems;
        }
        public CreateEmployeesDropdowns Edit(string? ID)
        {
            CreateEmployeesDropdowns dropdowns = InitViewModel();
            Employee data = dbContext.Employees?.FirstOrDefault(x => x.Id == ID);

            for (int i = 0; i < dropdowns.Managers.Count(); i++)
            {
                if (data.ManagerId == dropdowns.Managers[i].Value)
                {
                    dropdowns.Managers[i].Selected = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            for (int i = 0; i < dropdowns.Department.Count(); i++)
            {
                if (data.DepartmentId.ToString() == dropdowns.Department[i].Value)
                {
                    dropdowns.Department[i].Selected = true;
                    break;
                }
                else
                {
                    continue;
                }
            }

            dropdowns.Employee = uow.Mapper.Map<Insert_Update_EmployeeDto>(data);
            dropdowns.DepartmentId = dropdowns?.Employee?.DepartmentId?.ToString();
            dropdowns.ManagerId = dropdowns?.Employee?.ManagerId;
            return dropdowns;
        }
        public Insert_Update_EmployeeDto Edit(CreateEmployeesDropdowns model)
        {
            var res = uow.Employees.FirstOrDefault(x => x.Id == model.Employee.Id);
            if (res != null)
            {
                if (model.Employee.Image != null)
                    res.ImagePath = uow.helper.UploadImage(model.Employee.Image, $"{uow.WebRootPath}\\images\\");
                else
                    model.Employee.ImagePath = res.ImagePath;

                if (model.DepartmentId != null) res.DepartmentId = int.Parse(model.DepartmentId);
                if (model.ManagerId != null && model.ManagerId != "0") res.ManagerId = model.ManagerId; else res.ManagerId = null;

                res.Salay = model.Employee.Salay;
                res.FirstName = model.Employee.FirstName;
                res.LastName = model.Employee.LastName;

                uow.SaveChanges();
            }
            return model.Employee;
        }

        public CreateEmployeesDropdowns InitViewModel()
        {
            CreateEmployeesDropdowns dropdowns = new();
            dropdowns.Managers = uow.Employees.GetManagersSelectItems();
            dropdowns.Department = uow.Employees.GetDepartmentSelectItems();
            return dropdowns;
        }
        
        public void Delete(string id)
        {
            
            var manager = uow.Employees.FirstOrDefault(x => x.ManagerId == id); 
            if(manager == null)
            {
            var Employee = uow.Employees.FirstOrDefault(x => x.Id == id);
            if (Employee != null)
            {
                uow.Employees.Remove(Employee);
                uow.SaveChanges();
            }
            }
        }

        public async System.Threading.Tasks.Task CreateAsync(CreateEmployeesDropdowns model)
        {
            try
            {
                if (model.Employee.Image != null)
                    model.Employee.ImagePath = uow.helper.UploadImage(model.Employee.Image, $"{uow.WebRootPath}\\images\\");

                var employee = new Employee
                {
                    UserName = model.Employee.UserName,
                    FirstName = model.Employee.FirstName,
                    LastName = model.Employee.LastName,
                    Salay = model.Employee.Salay,
                    ImagePath = model.Employee.ImagePath,
                    ManagerId = model.ManagerId,
                    DepartmentId = int.Parse(model.DepartmentId)
                };

                var _Manager = await uow.userManager.FindByNameAsync(model.Employee.UserName);
                if (_Manager == null)
                {
                    await uow.userManager.CreateAsync(employee, model.Employee.Password);
                    await uow.userManager.AddToRolesAsync(employee, new List<string>
                {
                    Roles.Employee
                });
                }
                uow.SaveChanges();
            }
            catch(Exception ex)
            {

            }
           
        }

        public Employee GetEmployee(string userId)
        {
            Employee returnedEmployee = new();
            var employee = uow.Employees.Where(x => x.Id == userId).ToList();
            var tasks = uow.Tasks.GetAllTasks();
            var employeeTasks = dbContext.EmployeeTasks.ToList();

            var query = (from emp in employee
                        join emptask in employeeTasks on emp.Id equals emptask.EmployeeId
                        join task in tasks on emptask.TaskId equals task.Id
                        select new EmployeeTaskDto
                        {
                            EmployeeTaskId = emptask.Id,
                          Description = task.Description,
                          Title = task.Title,
                          Status = emptask.Status,
                          Id  =task.Id
                        } ).ToList();

            returnedEmployee = employee[0];
            returnedEmployee.Tasks = query;
            returnedEmployee.TaskStatus = uow.Tasks.GetTaskStatusSelectItems();
            
            foreach (var item in returnedEmployee.Tasks)
            {
                returnedEmployee.TasksDropDown.Add(new SelectListItem { Text = item.Title,Value=item.EmployeeTaskId.ToString()});
            }

            var selectedemptask = employeeTasks.Where(x => x.EmployeeId == userId).Select(x=>x.TaskId).ToList();
            var notSelectedTask = tasks.Where(x => !selectedemptask.Contains(x.Id));
            foreach (var item in notSelectedTask)
            {
                returnedEmployee.AllTasksDropDown.Add(new SelectListItem { Text = item.Title,Value=item.Id.ToString()});
            }
            return returnedEmployee;
        }

        public void ChangeTaskStatus(int emptaskid, TaskState status)
        {
            try
            {
                EmployeeTask res = dbContext.EmployeeTasks.Where(x => x.Id == emptaskid).FirstOrDefault();
                if (res != null)
                {

                res.Status = status;
                dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

           
        }
        
        public void AssignTask(string empid ,int taskid)
        {
            var task = dbContext.Tasks.Where(x => x.Id == taskid).FirstOrDefault();
            var emp = dbContext.Employees.Where(x => x.Id==empid).FirstOrDefault();

            if(task != null && emp != null)
            {
                dbContext.EmployeeTasks.Add(new EmployeeTask
                {
                    EmployeeId = empid,
                    TaskId = taskid,
                    Status = TaskState.New
                });
                dbContext.SaveChanges();
            }

          
        }
    }
     
}
