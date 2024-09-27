using Microsoft.AspNetCore.Mvc;
using Mvc.DataAccess.Data;
using Mvc.DataAccess.Respository;
using Mvc.DataAccess.Respository.IRepository;
using Mvc.Model;

namespace MvcApplicationWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categorylist = _unitOfWork.category.GetAll().ToList();
            return View(categorylist);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Input fields cannot be same");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.category.Add(category);
                _unitOfWork.Save();
                TempData["Success"] = "Caegory Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? selectedCategory = _unitOfWork.category.Get(u => u.Id == id); ;
            if (selectedCategory == null)
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
                _unitOfWork.category.Update(Obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null && id == 0)
            {
                return NotFound();
            }
            Category? category = _unitOfWork.category.Get(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}
