using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;
using Mvc.Model.ViewModels;
using System.Collections.Immutable;

namespace MvcApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _un;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork un, IWebHostEnvironment webHostEnvironment)
        {
            _un = un;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> productList = _un.product.GetAll(includeProperties:"Category").ToList();

            
            return View(productList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productvm = new ProductVM()
            {

                CategoryList= _un.product
                .GetAll(includeProperties:"Category").Select(u => new SelectListItem
                {
                    Text = u.Category.Name,
                    Value = u.Category.Id.ToString()
                }),
                product =new Product()
            };

            if (id==null || id==0)
            {
                return View(productvm);
            }
            else
            {
                productvm.product= _un.product.Get(u=>u.Id==id,includeProperties:"Category  ");
                return View(productvm);
            }
       
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productvm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName = Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");

                    if(!string.IsNullOrWhiteSpace(productvm.product.imageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath,productvm.product.imageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStream = new FileStream(Path.Combine(productPath,fileName),FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productvm.product.imageUrl= @"\images\product\"+fileName;
                }


                if(productvm.product.Id==0)
                {
                _un.product.Add(productvm.product);

                }
                else{
                    _un.product.Update(productvm.product);
                }
                _un.Save();
                TempData["Success"]="Product Created SuccessFully";
                return RedirectToAction("Index");
            }
            else
            {

                productvm.CategoryList= _un.category.GetAll(includeProperties:"Category")
                    .Select(u=>new SelectListItem
                    {
                        Text= u.Name,
                        Value =u.Id.ToString()
                    });
                return View(productvm);

            }
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            Product product = _un.product.Get(u => u.Id==id,includeProperties:"Category");
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

        public IActionResult Delete(int? id)
        {
            if(id==null||id==0)
            {
                return NotFound();
            }
           Product product=_un.product.Get(u=>u.Id==id,includeProperties:"Category");
            if( product==null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteItem(int? id)
        {
            if(id==null|| id==0)
            {
                return NotFound();
            }
           Product product= _un.product.Get(u=>u.Id==id,includeProperties:"Category");
           if(product==null)
           {
            return NotFound();
           }
           _un.product.Remove(product);
           _un.Save();
           TempData["success"]="Product deleted successfully";
           return RedirectToAction("Index");

        }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> productList = _un.product.GetAll(includeProperties:"Category").ToList();
        return Json(new{ data = productList});
    }

    #endregion
    }


    
}
