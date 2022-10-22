using BussinessLayer.Constants;
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

        public IActionResult Index(string id)
        {
            if(id == null) id= User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(uow.Employees.GetEmployee(id));
        }
        
        public IActionResult Details(string? id)
        {
            return View(uow.Employees.GetEmployee(id));
        }

        public IActionResult Create()
        {
            return View(uow.Employees.InitViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeesDropdowns model)
        {
           
            ModelState.Remove("Employee.Id");
            ModelState.Remove("Employee.ImagePath");
            ModelState.Remove("Employee.Image");
            ModelState.Remove("ManagerId");
            if (ModelState.IsValid)
            {
                model.Employee.Image = Request.Form.Files["ImageData"];
            model.ManagerId = User.FindFirstValue(ClaimTypes.NameIdentifier) as string;
                await uow.Employees.CreateAsync(model);
                return RedirectToAction("Index","Home");
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
        
        [HttpPost]
        public IActionResult ChangeStatusForAdmin(Employee model)  
        { 
            uow.Employees.ChangeTaskStatus(int.Parse(model.EmpTaskId.ToString()), model.SelectedTaskStatus);
            return RedirectToAction("Details", new { id = model.Id });
        }
        
        [HttpPost]
        public IActionResult Assign(Employee model)  
        { 
            uow.Employees.AssignTask(model.Id,model.SelectedTaskId);
            return RedirectToAction("Details", new { id = model.Id });
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
            ModelState.Remove("Employee.Password");
            ModelState.Remove("Employee.UserName");
            if (ModelState.IsValid)
            {
                model.Employee.Image = Request.Form.Files["ImageData"];
                uow.Employees.Edit(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(uow.Employees.Edit(model.Employee.Id));
            }

            //return View("Create", model.Employee);
        }


        public ActionResult Delete(string id)
        {
            if (User.IsInRole(Roles.Manager))
            {
                uow.Employees.Delete(id);
            }
                return RedirectToAction("Index", "Home");
        }


    }
}
