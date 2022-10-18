using AutoMapper;
using DataAccessLayer.Models;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MainProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public UserController(IUnitOfWork uow, IMapper mapper, IOptions<Helper> _helper)
        {
            this.uow = uow;
            this.mapper = mapper;
            helper = _helper.Value;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult GetUser()
        {
            var pageSize = int.Parse(Request.Form["length"]);
            var skip = int.Parse(Request.Form["start"]);

            var searchValue = Request.Form["search[value]"];

            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortColumnDirection = Request.Form["order[0][dir]"];

            IQueryable<Employee> User = uow.Employees.Query(m => string.IsNullOrEmpty(searchValue) ? true
                : (
                    m.FirstName.Contains(searchValue)        || 
                    m.LastName.Contains(searchValue)         || 
                    m.Manager.FirstName.Contains(searchValue)||
                    m.Manager.Salay.ToString().Contains(searchValue) ||
                    m.Manager.LastName.Contains(searchValue)
                   ));

            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            //    User = User.OrderBy(string.Concat(sortColumn, " ", sortColumnDirection));

            var QueryResult = User.Skip(skip).Take(pageSize).ToList();
            var data = mapper.Map<List<EmployeeDataDto>>(QueryResult);

            var recordsTotal = User.Count();

            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data };

            return Ok(jsonData);
        }

       
    }
}
