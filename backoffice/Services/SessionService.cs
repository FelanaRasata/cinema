using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace backoffice.Services;

public class SessionService
{
    private readonly ApplicationDbContext _context;

    public SessionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public DbSet<Session> FindSet()
    {
        return _context.Sessions;
    }

    public async Task<Session?> FindByIdAsync(int? id)
    {
        return await _context.Sessions
            .Include(s => s.Movie)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<int> SaveSession(Session session)
    {
        _context.Sessions.Add(session);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateSession(Session session)
    {
        _context.Attach(session).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    public async Task<int?> DeleteSession(int? id)
    {
        var session = await _context.Sessions.FindAsync(id);
        if (session != null)
        {
            _context.Sessions.Remove(session);
            return await _context.SaveChangesAsync();
        }

        return null;
    }

    public bool SessionExists(int id)
    {
        return _context.Sessions.Any(e => e.Id == id);
    }

    public async Task<int> uploadSession(IFormFile csvFile)
    {
        List<Session> sessions = new List<Session>();

        using (var reader = new StreamReader(csvFile.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                var session = new Session
                {
                    MovieId = int.Parse(values[0]),
                    SessionDateTime = DateTime.Parse(values[1]),
                    Room = int.Parse(values[2]),
                };

                sessions.Add(session);
            }
        }

        // Add sessions to database
        _context.Sessions.AddRange(sessions);
        return await _context.SaveChangesAsync();
    }

    public async Task<IPagedList<Session>> GetSessionsPaginate(int pageNumber, int pageSize)
    {
        var sessionsQuery = _context.Sessions
            .Include(s => s.Movie)
            .AsQueryable();

        return await sessionsQuery.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, pageSize);
    }
    
    public async Task<List<Session>> GetSessionsByMovie(int movieId)
    {
        return await _context.Sessions
            .Where(s => s.MovieId == movieId)
            .ToListAsync();
    }
    
    public  Task<int> countSession()
    {
        return  _context.Sessions.CountAsync();
    }
}