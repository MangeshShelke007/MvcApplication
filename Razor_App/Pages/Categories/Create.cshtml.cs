using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_App.DATA;
using Razor_App.Models;

namespace Razor_App.Pages.Categories
{

    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;


      
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _dbContext.Categories.Add(Category);
            _dbContext.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
