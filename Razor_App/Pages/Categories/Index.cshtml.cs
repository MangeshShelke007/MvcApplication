using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata;
using Razor_App.DATA;
using Razor_App.Models;
using System.Reflection.Metadata.Ecma335;

namespace Razor_App.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public List<Category> CategoryList { get; set; }
        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public void OnGet()
        {
            CategoryList= _dbContext.Categories.ToList(); 
        }
    }
}
