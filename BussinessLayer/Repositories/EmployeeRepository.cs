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

namespace BussinessLayer.Repository
{
    public interface IEmployeeRepository: IRepository<Employee>
    {
        List<SelectListItem> GetManagersSelectItems();
        List<SelectListItem> GetDepartmentSelectItems();
    }

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly DataBaseContext dbContext;

        public EmployeeRepository(DataBaseContext _DbContext ,IUnitOfWork uow) : base(_DbContext, uow)
        {
            dbContext = _DbContext;
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
    }
    
}
