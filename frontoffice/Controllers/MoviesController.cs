using frontoffice.Database;
using Microsoft.AspNetCore.Mvc;

namespace frontoffice.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieService _movieService;
    private readonly SessionService _sessionService;

    public MoviesController(MovieService movieService, SessionService sessionService)
    {
        _movieService = movieService;
        _sessionService = sessionService;
    }

    [HttpGet]
    public IActionResult Get(int pageNumber, int pageSize, string? keyword)
    {
        return Ok(_movieService.GetMovies(pageNumber, pageSize, keyword)); // Return all movies
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var movie = _movieService.GetMovie(id);
        if (movie == null)
        {
            return NotFound(); // Return 404 Not Found if movie is not found
        }

        return Ok(movie); // Return movie
    }

    [HttpGet("sessions/{id}")]
    public IActionResult GetSessionByMovieId(int id)
    {
        var sessions = _sessionService.GetSessionsByMovie(id);
        return Ok(sessions); // Return movie
    }
}