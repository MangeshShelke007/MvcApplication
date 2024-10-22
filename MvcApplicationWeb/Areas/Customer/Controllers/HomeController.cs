using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System.Diagnostics;
// 
namespace MvcApplicationWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _un;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork un)
        {
            _logger = logger;
            _un=un;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _un.product.GetAll(includePoperties:"Category");
            return View(products);
        }
        public IActionResult Detail(int id)
        {
            Product product = _un.product.Get(u=>u.Id==id,includeProperties:"Category");
            return View(product);
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
