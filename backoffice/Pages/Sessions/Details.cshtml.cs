using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Sessions
{
    public class DetailsModel : PageModel
    {
        private readonly SessionService _sessionService;

        public DetailsModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        public Session Session { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                TempData["error"] = "id not found!";
                return RedirectToPage("./Index");
            }

            var session = await _sessionService.FindByIdAsync(id);
            if (session == null)
            {
                TempData["error"] = $"Session {id} not found!";
                return RedirectToPage("./Index");
            }
            else
            {
                Session = session;
            }
            return Page();
        }
    }
}
