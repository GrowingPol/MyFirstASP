using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiPrimerASP.Model;

namespace MiPrimerASP.Pages.BookList
{
    public class IndexModel : PageModel
    {
        //Here we are using Dependency injection
        private readonly ApplicationDbContext _Db;
        //Constructor
        public IndexModel(ApplicationDbContext db) 
        {
            _Db = db;
        }
        public IEnumerable<Book> Books { get; set; }
        //Store Books in IEnumerable using Get Handler
        public async Task OnGet()
        {
            Books = await _Db.Book.ToListAsync();
        }//Now is available in view display

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var book = await _Db.Book.FindAsync(id);
            if(book != null)
            {
                _Db.Book.Remove(book);
                await _Db.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }
        }
    }
}
