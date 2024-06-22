using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserService _userService;

        public IndexModel(ILogger<IndexModel> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public new User? User { get; set; } = default!;

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

            return Page();
        }
    }
}