using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB;

namespace TaskAlbayan.Repository;
    public class TaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Task>> GetAll()
        {
            return await _context.tasks.ToListAsync();
        }

        public async Task<Task?> GetById(Guid id)
        {
            return await _context.tasks.FindAsync(id);
        }

        public async Task<Task> AddAsync(Task task)
        {
            return (await _context.tasks.AddAsync(task)).Entity;
        }

        public async Task<Task?> UpdteAsync(Task task)
        {
            if (await _context.tasks.AnyAsync(x => x.Id == task.Id))
            {
                return _context.tasks.Update(task).Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            var task = await _context.tasks.FindAsync(id);
            if(task != null)
            {
                return  _context.tasks.Remove(task).Entity.Id;
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
