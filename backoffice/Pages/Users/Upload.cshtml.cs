using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Users;

public class UploadModel : PageModel
{
    private readonly UserService _userService;

    public UploadModel(UserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> OnPostAsync(IFormFile csvFile)
    {
        if (csvFile == null || csvFile.Length == 0)
        {
            ModelState.AddModelError("csvFile", "Please select a CSV file.");
            return Page();
        }

        if (!csvFile.FileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            ModelState.AddModelError("csvFile", "File must be a CSV.");
            return Page();
        }

        try
        {
            await _userService.uploadUser(csvFile);

            TempData["success"] = "Users uploaded successfully!";
            return RedirectToPage("Index"); // Redirect to the homepage or another page
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error uploading file: {ex.Message}");
            return Page();
        }
    }
}