using backoffice.Databases;
using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace backoffice.Pages_Movies
{
    public class EditModel : PageModel
    {
        private readonly MovieService _movieService;
        private readonly ToolService _toolService;

        public EditModel(MovieService movieService, ToolService toolService)
        {
            _toolService = toolService;
            _movieService = movieService;
        }

        [BindProperty] public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.FindByIdAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewData["Categories"] = new SelectList(_toolService.GetCategories());
            Movie = movie;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _movieService.UpdateMovie(Movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_movieService.MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}