using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_App.DATA;
using Razor_App.Models;

namespace Razor_App.Pages.Categories
{


    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public Category? Category { get; set; }

        public DeleteModel(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public void OnGet(int? id)
        {
            if(id!=null && id!=0)
            {
                Category=_dbContext.Categories.Find(id);
            }
            

        }
        public IActionResult OnPost()
        {
            Category obj = _dbContext.Categories.Find(Category.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
