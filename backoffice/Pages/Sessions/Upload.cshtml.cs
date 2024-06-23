using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Sessions;

public class UploadModel : PageModel
{
    private readonly SessionService _sessionService;

    public UploadModel(SessionService sessionService)
    {
        _sessionService = sessionService;
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
            await _sessionService.uploadSession(csvFile);
            TempData["success"] = "Sessions uploaded successfully!";
            return RedirectToPage("Index"); 
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error uploading file: {ex.Message}");
            return Page();
        }
    }
}