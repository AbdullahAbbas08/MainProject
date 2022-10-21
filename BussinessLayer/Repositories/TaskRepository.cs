using BussinessLayer.UnitOfWork;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using DomainLayer.ViewModels;

using System.Web.Mvc;
using Task = DataAccessLayer.Models.Task;

namespace BussinessLayer.Repository
{
    
    public interface ITasksRepository: IRepository<DataAccessLayer.Models.Task>
    {
        List<SelectListItem> GetTaskStatusSelectItems();
        List<Task> GetAllTasks();

    }

    public class TasksRepository : Repository<DataAccessLayer.Models.Task>, ITasksRepository
    {
        private readonly DataBaseContext dbContext;
        private readonly IUnitOfWork uow;

        public TasksRepository(DataBaseContext _DbContext, IUnitOfWork uow) : base(_DbContext, uow)
        {
            dbContext = _DbContext;
            this.uow = uow;
        }

        //public CreateTaskDropdowns InitTaskViewModel()
        //{
        //    CreateTaskDropdowns dropdowns = new();
        //    //dropdowns.Employees = uow.Employees.GetEmployeeSelectItems();
        //    //dropdowns.TaskStatus = GetTaskStatusSelectItems();
        //    return dropdowns;
        //}

        public List<SelectListItem> GetTaskStatusSelectItems()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            foreach (int item in Enum.GetValues(typeof(TaskState)))
            {
                listItems.Add(new SelectListItem
                {
                    Value = item.ToString()
                });
            }

            int x = 0;
            foreach (string item in Enum.GetNames(typeof(TaskState)))
            {
                listItems[x].Text = item.ToString();
                x++;
            }

            return listItems;
        }


       public List<Task> GetAllTasks()
        {
            return dbContext.Tasks.ToList();

        }
    }
}
