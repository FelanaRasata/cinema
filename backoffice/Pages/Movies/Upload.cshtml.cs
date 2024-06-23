using backoffice.Databases;
using backoffice.Models;
using backoffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backoffice.Pages_Movies;

public class UploadModel : PageModel
{
    private readonly MovieService _movieService;

    public UploadModel(MovieService movieService)
    {
        _movieService = movieService;
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
            await _movieService.uploadMovie(csvFile);

            TempData["success"] = "Movies uploaded successfully!";
            return RedirectToPage("Index"); // Redirect to the homepage or another page
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error uploading file: {ex.Message}");
            return Page();
        }
    }
}