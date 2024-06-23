using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;
        private readonly SessionService _sessionService;
        private readonly MovieService _movieService;

        public IndexModel(UserService userService, MovieService movieService, SessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
            _movieService = movieService;
        }

        public new User? User { get; set; } = default!;
        public int countUsers = 0;
        public int countMovies = 0;
        public int countSessions = 0;

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("user");
            if (userId == null)
            {
                return RedirectToPage("./Login");
            }

            User = await _userService.FindByIdAsync(userId);
            if (User == null)
            {
                return RedirectToPage("./Login");
            }

            countMovies = await _movieService.countMovie();
            countSessions = await _sessionService.countSession();
            countUsers = await _userService.countUser();

            return Page();
        }
    }
}