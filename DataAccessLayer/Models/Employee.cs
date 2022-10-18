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

        public virtual List<Task> Tasks { get; set; }
    }
}
