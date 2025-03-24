using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB;

namespace TaskAlbayan.Repository;

    public class VisitTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public VisitTaskRepository(ApplicationDbContext context)
        {
            _context=context;
        }

        public async Task<List<VisitTask>>GetAll()
        {
            return await _context.visitTasks.ToListAsync();
        }

        public async Task<VisitTask?>GetById(Guid id)
        {
            return await _context.visitTasks.FindAsync(id);
        }

        public async Task<VisitTask> AddAsync (VisitTask visitTask)
        {
            return (await _context.visitTasks.AddAsync(visitTask)).Entity;
        }

        public async Task<VisitTask?>UpdateAsync (VisitTask visitTask)
        {
            if (await _context.visitTasks.AnyAsync(x=>x.Id == visitTask.Id))
            {
            return  _context.visitTasks.Update(visitTask).Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<Guid?>DeleteAsync(Guid id)
        {
            var detailsFound = await _context.visitTasks.FindAsync(id);
            if(detailsFound != null)
            {
                return  _context.visitTasks.Remove(detailsFound).Entity.Id;
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
