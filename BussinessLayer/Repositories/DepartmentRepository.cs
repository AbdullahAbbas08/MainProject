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
  
    public interface IDepartmentRepository: IRepository<Department>
    {
        void Create(string model);
         Department Edit(int ID);
        void Delete(int id);
         void Edit(Department Department); 
    }

    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {

        private readonly DataBaseContext dbContext;
        private readonly IUnitOfWork uow;

        public DepartmentRepository(DataBaseContext _DbContext, IUnitOfWork uow) : base(_DbContext, uow)
        {
            dbContext = _DbContext; 
            this.uow = uow;
        }

        public void Delete(int id)
        {
            var dept = uow.Employees.FirstOrDefault(x => x.DepartmentId == id);
            if (dept == null)
            {
                uow.Departments.Remove(uow.Departments.Find(id));
                uow.SaveChanges();
            }
        }

        public void Create(string model)
        { 
            uow.Departments.Add(new Department { Name = model});
            uow.SaveChanges();
        }

        public Department Edit(int ID)
        {
           
            return dbContext.Departments?.FirstOrDefault(x => x.Id == ID);
            

        }

        public void Edit(Department model)
        {
            var res = uow.Departments.FirstOrDefault(x => x.Id == model.Id);
            if (res != null)
            {
              res.Name = model.Name;
                uow.SaveChanges();
            }
        }
    }  
}
