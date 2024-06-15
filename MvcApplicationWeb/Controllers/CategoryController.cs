using Microsoft.AspNetCore.Mvc;
using Mvc.DataAccess.Data;
using Mvc.Model;

namespace MvcApplicationWeb.Controllers
{
    //23k23k2p32qjoiwqejowajeowarjiwarjiowandkjwandjkawnjodwanjd
    //oiwqejoiwqejoiwqjeoiwqjeoqwjo
    //new changes
    //13.08
    //13.12
    
    
    
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbcontext=dbContext;
        }
        public IActionResult Index()
        {
            List<Category> categorylist = _dbcontext.Categories.ToList();
            return View(categorylist);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name==category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Input fields cannot be same");
            }

            if (ModelState.IsValid)
            {
                _dbcontext.Categories.Add(category);
                _dbcontext.SaveChanges();
                TempData["Success"]="Caegory Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id== 0)
            {
                return NotFound();
            }
            Category? selectedCategory = _dbcontext.Categories.Find(id);
            if(selectedCategory == null)
            {
                return NotFound();
            }
            return View(selectedCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category Obj)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Categories.Update(Obj);
                _dbcontext.SaveChanges();
                TempData["Success"]="Category Updated Successfully";
                return(RedirectToAction("Index"));
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id==null&& id == 0)
            {
                return NotFound();
            }
            Category? category= _dbcontext.Categories.Find(id);

            if(category==null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _dbcontext.Categories.Find(id);
            if (obj==null)
            {
                return NotFound();
            }
            _dbcontext.Categories.Remove(obj);
            _dbcontext.SaveChanges();
            TempData["Success"]="Category Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}
