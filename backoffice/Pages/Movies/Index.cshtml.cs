using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using X.PagedList;

namespace backoffice.Pages_Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieService _movieService;

        public IndexModel(MovieService movieService)
        {
            _movieService = movieService;
        }

        public IPagedList<Movie> Movies { get; set; }
        public string Keyword { get; set; }

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 10, string keyword ="")
        {
            Keyword = keyword;
            Movies = await _movieService.GetMoviesPaginate(pageNumber, pageSize, keyword);
        }

        
    }
}