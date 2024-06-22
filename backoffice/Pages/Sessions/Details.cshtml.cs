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
                return NotFound();
            }

            var session = await _sessionService.FindByIdAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            else
            {
                Session = session;
            }
            return Page();
        }
    }
}
