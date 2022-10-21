using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DomainLayer.ViewModels
{
    public class CreateEmployeesDropdowns
    {
        public CreateEmployeesDropdowns()
        {
            Managers = new();
            Department = new();
            Employee = new();
        }
        public List<SelectListItem> Managers { get; set; }
        public List<SelectListItem> Department { get; set; }
        public Insert_Update_EmployeeDto Employee { get; set; }
        public string DepartmentId { get; set; }
        public string ManagerId { get; set; }
    }
}
 