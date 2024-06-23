using backoffice.Databases;
using backoffice.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace backoffice.Services;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
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

    public async Task<int> uploadUser(IFormFile csvFile)
    {
        List<User> movies = new List<User>();

        using (var reader = new StreamReader(csvFile.OpenReadStream()))
        {
            while (reader.Peek() >= 0)
            {
                var line = await reader.ReadLineAsync();
                var values = line.Split(',');

                var movie = new User
                {
                    Name = values[0],
                    Email = values[1],
                    Password = values[2]
                };

                movies.Add(movie);
            }
        }

        // Add movies to database
        _context.Users.AddRange(movies);
        return await _context.SaveChangesAsync();
    }
    
    public async Task<IPagedList<User>> GetUsersPaginate(int pageNumber, int pageSize, string keyword)
    {
        var usersQuery = _context.Users.AsQueryable();
        if (!string.IsNullOrEmpty(keyword))
        {
            usersQuery = usersQuery.Where(m =>
                m.Name.Contains(keyword) || m.Email.Contains(keyword));
        }
        return await usersQuery.OrderBy(m => m.Id).ToPagedListAsync(pageNumber, pageSize);
    }
    
    public  Task<int> countUser()
    {
        return  _context.Users.CountAsync();
    }
}