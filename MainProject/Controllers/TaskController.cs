using Microsoft.AspNetCore.Mvc;
using Task = DataAccessLayer.Models.Task;

namespace MainProject.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUnitOfWork uow;

        public TaskController(IUnitOfWork uow)
        {
            this.uow = uow;
        } 

        public IActionResult Index()
        {
            var res = uow.Tasks.GetAllTasks();
            return View(res);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateTaskDropdowns model)
        {
           
            if (ModelState.IsValid)
            {
                var taskdto = uow.Mapper.Map<Task>(model.task);
                //TaskState myStatus;
                //Enum.TryParse(model.SelectedTaskStatus, out myStatus);
                uow.Tasks.Add(taskdto);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }

            //CreateTaskDropdowns dropdowns = uow.Tasks.InitTaskViewModel();
            return View();
        }

    }
}
