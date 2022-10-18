namespace DataAccessLayer.Models
{
    [Table(name: "Departments", Schema = "dbo")]
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "PleaseEnterDepartmentName")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "DepartmentName")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
