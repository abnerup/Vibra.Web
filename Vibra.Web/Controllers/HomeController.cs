using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Vibra.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContext;

        public HomeController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            var user =  _context.Users.FirstOrDefault(d => d.Email == User.Identity.Name);
            if (user != null) 
            {
                _httpContext.HttpContext.Session.SetString("UserId", user.Id);
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}