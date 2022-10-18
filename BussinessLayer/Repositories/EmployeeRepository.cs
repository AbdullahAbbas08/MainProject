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
    public interface IEmployeeRepository: IRepository<Employee>
    {

    }

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DataBaseContext _DbContext ,IUnitOfWork uow) : base(_DbContext, uow)
        {

        }

    }
    
}
