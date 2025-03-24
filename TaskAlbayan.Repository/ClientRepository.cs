using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskAlbayan.DB;

namespace TaskAlbayan.Repository;

    public class ClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAll()
        {
            return await _context.clients.ToListAsync();
        }

        public async Task<Client?> GetById(Guid id)
        {
            return await _context.clients.FindAsync(id);
        }

        public async Task<Client> AddAsync(Client client)
        {
            return (await _context.clients.AddAsync(client)).Entity;
        }

        public async Task<Client?> UpdateAsync(Client client)
        {
            if (await _context.clients.AnyAsync(x => x.Id == client.Id))
            {
                return _context.clients.Update(client).Entity;
            }
            else
            {
                return null;
            }
        }


        public async Task<Guid?> DeleteAsync(Guid id)
        {
            var client = await _context.clients.FindAsync(id);
            if (client != null)
            {
                return _context.clients.Remove(client).Entity.Id;
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
