using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using parcial.Data;
using parcial.Models;
using parcial.Repository.Interface;

namespace parcial.Repository.Manager
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;

        public MarcaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Marca>> GetAll()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca> GetById(int id)
        {
            return await _context.Marcas.FindAsync(id);
        }

        public async Task Add(Marca marca)
        {
            await _context.Marcas.AddAsync(marca);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Marca marca)
        {
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Marcas.AnyAsync(e => e.MarcaId == id);
        }
    }
}
