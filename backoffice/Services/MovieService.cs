using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace backoffice.Services;

public class MovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<Movie> FindSet()
    {
        return _context.Movies;
    }

    public async Task<Movie?> FindByIdAsync(int? id)
    {
        return await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> SaveMovie(Movie movie)
    {
        _context.Movies.Add(movie);
        return await _context.SaveChangesAsync();
    }


    public async Task<int> UpdateMovie(Movie movie)
    {
        _context.Attach(movie).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    public async Task<int?> DeleteMovie(int? id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie != null)
        {
            _context.Movies.Remove(movie);
            return await _context.SaveChangesAsync();
        }

        return null;
    }

    public bool MovieExists(int id)
    {
        return _context.Movies.Any(e => e.Id == id);
    }

    public async Task<int> uploadMovie(IFormFile csvFile)
    {
        List<Movie> movies = new List<Movie>();

        using (var reader = new StreamReader(csvFile.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                var movie = new Movie
                {
                    Title = values[0],
                    Description = values[1],
                    Duration = int.Parse(values[2]),
                    Category = values[3]
                };

                movies.Add(movie);
            }
        }

        // Add movies to database
        _context.Movies.AddRange(movies);
        return await _context.SaveChangesAsync();
    }

    public async Task<IPagedList<Movie>> GetMoviesPaginate(int pageNumber, int pageSize, string keyword)
    {
        var moviesQuery = _context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
        {
            moviesQuery = moviesQuery.Where(m =>
                m.Title.Contains(keyword) || m.Description.Contains(keyword) || m.Category.Contains(keyword));
        }

        return await moviesQuery.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, pageSize);
    }
    
    public  Task<int> countMovie()
    {
        return  _context.Movies.CountAsync();
    }
}