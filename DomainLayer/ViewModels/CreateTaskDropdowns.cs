using System.Web.Mvc;

namespace DomainLayer.ViewModels
{
    public class CreateTaskDropdowns
    {
        public CreateTaskDropdowns()
        {
            task = new();
            //Employees = new();
            //TaskStatus = new();
        }

        public TaskDto task { get; set; }
        
        //public string EmployeeId { get; set; } 
        //public string SelectedTaskStatus { get; set; }  
        //public List<SelectListItem> Employees { get; set; }
        //public List<SelectListItem> TaskStatus { get; set; }
    }

} 
