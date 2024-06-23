using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Sessions
{
    public class DeleteModel : PageModel
    {
        private readonly SessionService _sessionService;

        public DeleteModel(SessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [BindProperty] public Session Session { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _sessionService.DeleteSession(id);
            TempData["success"] = "Session deleted successfully!";
            return RedirectToPage("./Index");
        }
    }
}