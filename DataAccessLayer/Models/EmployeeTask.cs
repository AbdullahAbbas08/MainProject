namespace DataAccessLayer.Models
{
    public class EmployeeTask
    {
        public int Id { get; set; }

        public TaskState Status { get; set; }


        public string EmployeeId { get; set; }
        public int TaskId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; } 
    }

    public enum TaskState
    {
        [Display(Name = "New Task")]
        New,
        [Display(Name = "InProgress Task")]
        InProgress,
        [Display(Name = "Finished Task")]
        Finished
    }

} 
