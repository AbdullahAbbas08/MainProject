using System.Security.Claims;

namespace MainProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork uow;

        public EmployeeController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IActionResult Index()
        { 
            return View(uow.Employees.GetEmployee(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        }

        public IActionResult Create()
        {
            return View(uow.Employees.InitViewModel());
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeesDropdowns model)
        {
           
            ModelState.Remove("Employee.Id");
            ModelState.Remove("Employee.ImagePath");
            ModelState.Remove("Employee.Image");

            if (ModelState.IsValid)
            {
                model.Employee.Image = Request.Form.Files["ImageData"];
                uow.Employees.Create(model);
                return RedirectToAction("Index");
            }
            CreateEmployeesDropdowns dropdowns = uow.Employees.InitViewModel();
            dropdowns.Employee = model.Employee;

            return View(dropdowns);
        }
        
        [HttpPost]
        public IActionResult ChangeStatus(Employee model) 
        { 
            uow.Employees.ChangeTaskStatus(int.Parse(model.EmpTaskId.ToString()), model.SelectedTaskStatus);
            return RedirectToAction("Index");
        }


        public IActionResult Edit(string? ID)
        {
            return View(uow.Employees.Edit(ID));
        }

        [HttpPost]
        public IActionResult Edit(CreateEmployeesDropdowns model)
        {
            ModelState.Remove("Employee.Id");
            ModelState.Remove("Employee.ImagePath");
            ModelState.Remove("Employee.Image");
            if (ModelState.IsValid)
            {
                model.Employee.Image = Request.Form.Files["ImageData"];
                uow.Employees.Edit(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(uow.Employees.Edit(model.Employee.Id));
            }

            //return View("Create", model.Employee);
        }


        public ActionResult Delete(string id)
        {
            try
            {
                uow.Employees.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json("error");
            }

        }


    }
}
