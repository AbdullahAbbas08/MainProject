
namespace MainProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public EmployeeController(IUnitOfWork uow, 
                                  IMapper mapper,
                                  IOptions<Helper> _helper)
        {
            this.uow = uow;
            this.mapper = mapper;
            helper = _helper.Value;
        }
         
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            CreateEmployeesDropdowns dropdowns = new();
            dropdowns.Managers = uow.Employees.GetManagersSelectItems();
            dropdowns.Department = uow.Employees.GetDepartmentSelectItems();

            return View(dropdowns);
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeesDropdowns model)
        {
            CreateEmployeesDropdowns dropdowns = new();
            dropdowns.Managers = uow.Employees.GetManagersSelectItems();
            dropdowns.Department = uow.Employees.GetDepartmentSelectItems();

            ModelState.Remove("Id");
            ModelState.Remove("ImagePath");

            if (ModelState.IsValid)
            {
                model.Employee.Image = Request.Form.Files["ImageData"];
                if (model.Employee.Image != null)
                    model.Employee.ImagePath = helper.UploadImage(model.Employee.Image);
                else
                    View("create", model);

                Employee employeeData = mapper.Map<Employee>(model.Employee);
                uow.Employees.Add(employeeData);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(dropdowns);

        }

      

    }
}
