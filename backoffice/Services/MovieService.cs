using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;

namespace backoffice.Services;

public class MovieService
{
    private readonly ApplicationDbContext _context;

    public MovieService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> FindAllAsync()
    {
        return await _context.Movies.ToListAsync();
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
}