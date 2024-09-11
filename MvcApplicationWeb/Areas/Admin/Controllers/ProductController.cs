using Microsoft.AspNetCore.Mvc;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using System.Collections.Immutable;

namespace MvcApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _un;
        public ProductController(IUnitOfWork un)
        {
            _un = un;
        }

        public IActionResult Index()
        {
            List<Product> productList = _un.product.GetAll().ToList();
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _un.product.Add(product);
                _un.Save();
                TempData["Success"]="Product Created SuccessFully";
                return RedirectToAction("Index");

            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product product = _un.product.Get(u => u.Id==id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                 _un.product.Update(product);
                 _un.Save();
                 TempData["Success"]= "Product updated successfully";
                 return RedirectToAction("Index");

            }
            return View();
           
        }
    }
}
