namespace MainProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly Helper helper;

        public HomeController(IUnitOfWork uow, IMapper mapper, IOptions<Helper> _helper)
        {
            this.uow = uow;
            this.mapper = mapper;
            helper = _helper.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}