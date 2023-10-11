using BoardGameNight.Data;
using BoardGameNight.Models;
using BoardGameNight.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoardGameNight.Repositories.Implementations
{
    public class EtenRepository : IEtenRepository
    {
        private readonly ApplicationDbContext _context;

        public EtenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Eten>> GetAllAsync()
        {
            return await _context.Eten.ToListAsync();
        }

        public async Task<Eten> GetByIdAsync(int id)
        {
            return await _context.Eten.FindAsync(id);
        }

        public async Task CreateAsync(Eten eten)
        {
            _context.Eten.Add(eten);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Eten eten)
        {
            _context.Entry(eten).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eten = await _context.Eten.FindAsync(id);
            if (eten != null)
            {
                _context.Eten.Remove(eten);
                await _context.SaveChangesAsync();
            }
        }
    }
}