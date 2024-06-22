using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;

namespace backoffice.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> FindAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public DbSet<User> FindSet()
    {
        return _context.Users;
    }

    public async Task<User?> FindByIdAsync(int? id)
    {
        return await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<int> SaveUser(User user)
    {
        _context.Users.Add(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateUser(User user)
    {
        _context.Attach(user).State = EntityState.Modified;
        return await _context.SaveChangesAsync();
    }

    public async Task<int?> DeleteUser(int? id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync();
        }

        return null;
    }

    public bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }

    public Task<User?> login(string email, string password)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
    }
}