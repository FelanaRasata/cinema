using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backoffice.Pages_Movies
{
    public class CreateModel : PageModel
    {
        private readonly MovieService _movieService;
        private readonly ToolService _toolService;

        public CreateModel(MovieService movieService, ToolService toolService)
        {
            _movieService = movieService;
            _toolService = toolService;
        }

        public IActionResult OnGet()
        {
            ViewData["Categories"] = new SelectList(_toolService.GetCategories());
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _movieService.SaveMovie(Movie);

            return RedirectToPage("./Index");
        }
    }
}
