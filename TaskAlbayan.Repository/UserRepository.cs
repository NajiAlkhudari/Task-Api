using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB;
using TaskAlbayan.DB.Models;

namespace TaskAlbayan.Repository;

public class UserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        return (await _context.Users.AddAsync(user)).Entity;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        if (await _context.Users.AnyAsync(u => u.Id == user.Id))
        {
            return _context.Users.Update(user).Entity;
        }
        else
        {
            return null;
        }
    }

    public async Task<Guid?> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            return user.Id;
        }
        else
        {
            return null;
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}