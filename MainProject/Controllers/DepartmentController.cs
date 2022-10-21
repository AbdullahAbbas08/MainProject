using Microsoft.AspNetCore.Mvc;

namespace MainProject.Controllers
{
    public class DepartmentApiController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public DepartmentApiController(IUnitOfWork uow,
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
        public IActionResult Create(string model)
        {

            if (ModelState.IsValid)
            {

                uow.Departments.Add(new Department { Name = model});
                uow.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try 
            {
                var obj = uow.Departments.FirstOrDefault(x => x.Id == id); 
                if (obj != null)
                {
                    uow.Departments.Remove(obj);
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
