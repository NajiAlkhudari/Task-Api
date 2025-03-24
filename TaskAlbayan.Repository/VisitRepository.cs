using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB;

namespace TaskAlbayan.Repository;
    public class VisitRepository
    {
        private readonly ApplicationDbContext _context;
        public VisitRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Visit>> GetAll()
        {
            return await _context.visits.
            Include(x=>x.visitTasks)
            .ThenInclude(x=>x.task)
            .ToListAsync();

        }

        public async Task<Visit?>GetById(Guid id)
        {
            return await _context.visits.FindAsync(id);
        }

        public async Task<Visit> AddAsync(Visit visit)
        {
            return (await _context.visits.AddAsync(visit)).Entity;
        }

        public async Task<Visit?> UpdateAsync(Visit visit)
        {
            if(await _context.visits.AnyAsync(x=>x.Id == visit.Id))
            {
                return _context.visits.Update(visit).Entity;
            }
            else
            {
                return null;
            }
        }

        public async Task<Guid?>DeleteAsync(Guid id)
        {
            var visitDeat = await _context.visits.FindAsync(id);
            if(visitDeat != null)
            {
                return _context.visits.Remove(visitDeat).Entity.Id;
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
