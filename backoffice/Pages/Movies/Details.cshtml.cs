using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Movies
{
    public class DetailsModel : PageModel
    {
        private readonly MovieService _movieService;

        public DetailsModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        public Movie Movie { get; set; } = default!;

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
            else
            {
                Movie = movie;
            }

            return Page();
        }
    }
}