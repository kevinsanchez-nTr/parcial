using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using parcial.Data;
using parcial.Models;
using parcial.Repository.Interface;

namespace parcial.Repository.Manager
{
    public class VentaRepository : IVentaRepository
    {
        private readonly ApplicationDbContext _context;

        public VentaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> GetAll()
        {
            return await _context.Ventas.Include(v => v.Vehiculo).ToListAsync();
        }

        public async Task<Venta> GetById(int id)
        {
            return await _context.Ventas.Include(v => v.Vehiculo)
                                        .FirstOrDefaultAsync(v => v.VentaId == id);
        }

        public async Task Add(Venta venta)
        {
            await _context.Ventas.AddAsync(venta);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Venta venta)
        {
            _context.Ventas.Update(venta);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Ventas.AnyAsync(v => v.VentaId == id);
        }
    }
}
