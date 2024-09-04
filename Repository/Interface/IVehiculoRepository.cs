using System.Collections.Generic;
using System.Threading.Tasks;
using parcial.Models;

namespace parcial.Repository.Interface
{
    public interface IVehiculoRepository
    {
        Task<IEnumerable<Vehiculo>> GetAll();
        Task<Vehiculo> GetById(int id);
        Task Add(Vehiculo vehiculo);
        Task Update(Vehiculo vehiculo);
        Task Delete(int id);
        Task<bool> Exists(int id);
    }
}
