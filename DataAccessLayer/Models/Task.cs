namespace DataAccessLayer.Models
{
    [Table(name: "Tasks", Schema = "dbo")]
    public class Task
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "PleaseEnterTitleOfTask")]
        [MaxLength(50, ErrorMessage = "Maximum50Characters")]
        [Display(Name = "TaskTitle")]
        public string Title { get; set; }
        
        
        
        [Required(ErrorMessage = "PleaseEnterTitleOfDescription")]
        [Display(Name = "TaskDescription")]
        public string Description  { get; set; }


        public TaskStatus Status { get; set; }


        public DateTime? StartDate { get; set; }


        public DateTime? EndtDate { get; set; }

        public string EmployeeId { get; set; } 


        public virtual Employee Employee { get; set; }

    }
} 
