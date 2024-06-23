using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backoffice.Pages_Sessions
{
    public class CreateModel : PageModel
    {
        private readonly MovieService _movieService;
        private readonly SessionService _sessionService;
        private readonly ToolService _toolService;

        public CreateModel(MovieService movieService, SessionService sessionService, ToolService toolService)
        {
            _movieService = movieService;
            _sessionService = sessionService;
            _toolService = toolService;
        }

        public IActionResult OnGet()
        {
            ViewData["MovieId"] = new SelectList(_movieService.FindSet(), "Id", "Title");
            ViewData["Rooms"] = new SelectList(_toolService.GetNumbers());
            return Page();
        }

        [BindProperty] public Session Session { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _sessionService.SaveSession(Session);
            TempData["success"] = "Session added successfully!";
            return RedirectToPage("./Index");
        }
    }
}