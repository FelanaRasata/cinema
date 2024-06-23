using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Users;

public class SignOutModel : PageModel
{
    private readonly UserService _userService;

    public SignOutModel(UserService userService)
    {
        _userService = userService;
    }

    public new User? User { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        int? userId = HttpContext.Session.GetInt32("user");
        if (userId == null)
        {
            return RedirectToPage("/Login");
        }

        User = await _userService.FindByIdAsync(userId);
        if (User == null)
        {
            return RedirectToPage("/Login");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        HttpContext.Session.Remove("user");
        TempData["success"] = "Signed out successfully!";
        return RedirectToPage("/Login");
    }
}