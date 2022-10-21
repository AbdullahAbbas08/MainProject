using System.Web.Mvc;

namespace DataAccessLayer.Models
{
    [Table(name: "Employees", Schema = "dbo")]
    public class Employee: AppUser
    {
       
        [Required(ErrorMessage = "Please Insert Salay")]
        [Column(TypeName = "decimal(10,5)")]
        [Range(1000,50000, ErrorMessage = "Salay Must be between 1000 EGP to 50000 EGP ")]
        public decimal Salay { get; set; }

        public int? DepartmentId { get; set; }


        public string? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }

        [NotMapped]
        public string ManagerFullName 
        {
            get
            {
                if(Manager != null) return Manager.FullName;
                else return "No Manager";
            }
        }
        public virtual Department Department { get; set; }

        [NotMapped]
        public List<EmployeeTaskDto> Tasks { get; set; }

        [NotMapped]
        public TaskState SelectedTaskStatus { get; set; }
        [NotMapped]
        public List<SelectListItem> TaskStatus { get; set; }

        [NotMapped]
        public List<SelectListItem> TasksDropDown { get; set; } = new List<SelectListItem>();

        [NotMapped]
        public int EmpTaskId { get; set; }

    }

    public class EmployeeTaskDto
    {

        public int Id { get; set; }
        public int EmployeeTaskId { get; set; } 
        public TaskState Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }

} 
