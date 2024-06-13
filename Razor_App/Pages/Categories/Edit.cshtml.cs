using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_App.DATA;
using Razor_App.Models;

namespace Razor_App.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public Category? Category { get; set; }

        public EditModel(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void OnGet(int? id)
        {
            if (id!=null && id != 0)
            {
                Category=_dbContext.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(Category);
                _dbContext.SaveChanges();
              //  TempData["Success"]="Category Updated Successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
