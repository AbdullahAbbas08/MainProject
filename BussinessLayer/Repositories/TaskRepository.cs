using BussinessLayer.UnitOfWork;
using DataAccessLayer.DbContext;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLayer.Repository
{
    
    public interface ITasksRepository: IRepository<DataAccessLayer.Models.Task>
    {

    }

    public class TasksRepository : Repository<DataAccessLayer.Models.Task>, ITasksRepository
    {
        public TasksRepository(DataBaseContext _DbContext, IUnitOfWork uow) : base(_DbContext, uow)
        {
             
        }
    }
}
