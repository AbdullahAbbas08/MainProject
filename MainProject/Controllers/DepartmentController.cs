using Microsoft.AspNetCore.Mvc;

namespace MainProject.Controllers
{
    public class DepartmentController : Controller 
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public DepartmentController(IUnitOfWork uow,
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
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                uow.Departments.Create(model.Name);  
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int ID)
        {
            return View(uow.Departments.Edit(ID));
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            //ModelState.Remove("Employee.Id");
            //ModelState.Remove("Employee.ImagePath");
            //ModelState.Remove("Employee.Image");
            if (ModelState.IsValid)
            {
                uow.Departments.Edit(model);
                return RedirectToAction("Index");
            }
            else
            {
                return View(uow.Departments.Edit(model.Id));
            }
        }



        public ActionResult Delete(int id)
        {
            try 
            {
                var obj = uow.Departments.FirstOrDefault(x => x.Id == id); 
                if (obj != null)
                {
                    uow.Departments.Delete(obj.Id); 
                    uow.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json("error");
            }

        }
    }
}
