using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace backoffice.Pages_Movies
{
    public class DetailsModel : PageModel
    {
        private readonly MovieService _movieService;
        private readonly SessionService _sessionService;

        public DetailsModel(MovieService movieService, SessionService sessionService)
        {
            _movieService = movieService;
            _sessionService = sessionService;
        }

        public Movie Movie { get; set; } = default!;
        public List<Session> Sessions { get; set; }

        public async Task<IActionResult> OnGetAsync(int id, int pageNumber = 1, int pageSize = 10)
        {
            var movie = await _movieService.FindByIdAsync(id);
            if (movie == null)
            {
                TempData["error"] = $"Movie {id} not found!";
                return RedirectToPage("./Index");
            }
            else
            {
                Movie = movie;
            }


            Sessions = await _sessionService.GetSessionsByMovie(id);

            return Page();
        }
    }
}