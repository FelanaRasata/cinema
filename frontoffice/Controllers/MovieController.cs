using System.Text;
using backoffice.Models;
using frontoffice.Database;
using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using X.PagedList;

namespace frontoffice.Controllers;

public class MovieController : Controller
{
    private readonly MovieService _movieService;
    private readonly SessionService _sessionService;
    private readonly PdfService _pdfService;

    public MovieController(MovieService movieService, SessionService sessionService, PdfService pdfService)
    {
        _movieService = movieService;
        _sessionService = sessionService;
        _pdfService = pdfService;
    }

    // GET
    public IActionResult Index(int? pageNumber, int? pageSize, string keyword = null)
    {
        int number = pageNumber ?? 1;
        int size = pageSize ?? 9;
        
        ViewBag.Keyword = keyword;

        var movies = _movieService.GetMovies(number, size,keyword);
        var totalMovies = _movieService.CountMovies(keyword);

        var pagedMovies = new StaticPagedList<Movie>(movies, number, size, totalMovies);
        return View(pagedMovies);
    }

    // GET
    public async Task<IActionResult> Details(int id)
    {
        var movie = _movieService.GetMovie(id)!;
        var sessions = _sessionService.GetSessionsByMovie(id);

        var viewModel = new MovieDetailsViewModel
        {
            Movie = movie,
            Sessions = sessions
        };

        return View(viewModel);
    }

    public IActionResult ExportToPdf()
    {
        // Get the list of movies (example)
        List<Movie> movies = _movieService.GetAllMovies(); // Replace with your actual method


        // Save the document to a memory stream and return as a file download
        MemoryStream stream = _pdfService.ExportToPdf(movies);

        return File(stream, "application/pdf", "Movies.pdf");
    }
}

public class MovieDetailsViewModel
{
    public Movie Movie { get; set; }
    public List<Session> Sessions { get; set; }
}