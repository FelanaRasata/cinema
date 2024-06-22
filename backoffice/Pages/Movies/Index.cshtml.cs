using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieService _movieService;

        public IndexModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IList<Movie> Movie { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Movie = await _movieService.FindAllAsync();
        }
    }
}