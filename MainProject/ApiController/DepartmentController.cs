using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var res = uow.Departments.DbSet.Include(s => s.Employees).Where(m=> string.IsNullOrEmpty(searchValue) ? true
                : (m.Name.Contains(searchValue))); 

            List<DepartmentDto> deptlist = new List<DepartmentDto>();
            foreach (var item in res)
            {
                var resItem = new DepartmentDto 
                {
                    Id = item.Id,
                    Name = item.Name,
                    EmployeeCount = item.Employees.Count(),
                    SumSalary = item.Employees.Sum(s => s.Salay)
                };
                deptlist.Add(resItem);
            }



            var data = deptlist.Skip(skip).Take(pageSize).ToList();
            var recordsTotal = Departments.Count();
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);
        }
    }
}
