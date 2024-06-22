using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages;

public class LoginModel : PageModel
{
    private readonly UserService _userService;

    public LoginModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty] public string Email { get; set; } = "admin@gmail.com";
    [BindProperty] public string Password { get; set; } = "123456789";

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userService.login(Email, Password);
        if (user == null)
        {
            return Page();
        }

        HttpContext.Session.SetInt32("user", user.Id);
        return RedirectToPage("./Index");
    }
}