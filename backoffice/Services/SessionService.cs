using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;

namespace backoffice.Services;

public class SessionService
{
    private readonly ApplicationDbContext _context;

    public SessionService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Session>> FindAllAsync()
    {
        return await _context.Sessions
            .Include(s => s.Movie).ToListAsync();
    }

    public DbSet<Session> FindSet()
    {
        return _context.Sessions;
    }

    public async Task<Session?> FindByIdAsync(int? id)
    {
        return await _context.Sessions.FirstOrDefaultAsync(s => s.Id == id);
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
}