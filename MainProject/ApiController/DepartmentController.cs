using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MainProject.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public DepartmentController(IUnitOfWork uow, IMapper mapper, IOptions<Helper> _helper)
        {
            this.uow = uow;
            this.mapper = mapper;
            helper = _helper.Value; 
        }
       
        [HttpPost]
        public IActionResult GetDeparments()  
        {
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"];

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            IQueryable<Department> Departments = uow.Departments.Query(m => string.IsNullOrEmpty(searchValue) ? true
                : ( m.Name.Contains(searchValue) ));

            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //    User = User.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));
            var emplyees = uow.Employees.DbSet;
            var query = (from emp in emplyees
                        join dept in Departments
                        on emp.DepartmentId equals dept.Id
                        group dept by new {dept.Id,emp.Salay} 
                        into g
                        select new DepartmentDto
                        {
                            Id = g.Key.Id,
                            EmployeeCount = g.Count(),
                            Name = g.FirstOrDefault().Name,
                            SumSalary = g.Key.Salay
                        }).ToList();
            var data = query.Skip(skip).Take(pageSize).ToList();
            var recordsTotal = Departments.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);
        }
    }
}
