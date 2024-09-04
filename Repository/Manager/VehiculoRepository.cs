using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using parcial.Data;
using parcial.Models;
using parcial.Repository.Interface;

namespace parcial.Repository.Manager
{
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly ApplicationDbContext _context;

        public VehiculoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehiculo>> GetAll()
        {
            return await _context.Vehiculos.Include(v => v.Marca).ToListAsync();
        }

        public async Task<Vehiculo> GetById(int id)
        {
            return await _context.Vehiculos.Include(v => v.Marca).FirstOrDefaultAsync(v => v.VehiculoId == id);
        }

        public async Task Add(Vehiculo vehiculo)
        {
            await _context.Vehiculos.AddAsync(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Vehiculo vehiculo)
        {
            _context.Vehiculos.Update(vehiculo);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo != null)
            {
                _context.Vehiculos.Remove(vehiculo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Vehiculos.AnyAsync(v => v.VehiculoId == id);
        }
    }
}
