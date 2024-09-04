using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using parcial.Models;
using parcial.Repository.Interface;

namespace parcial.Controllers
{
    public class VehiculoesController : Controller
    {
        private readonly IVehiculoRepository _vehiculoRepository;
        private readonly IMarcaRepository _marcaRepository;

        public VehiculoesController(IVehiculoRepository vehiculoRepository, IMarcaRepository marcaRepository)
        {
            _vehiculoRepository = vehiculoRepository;
            _marcaRepository = marcaRepository;
        }

        // GET: Vehiculoes
        public async Task<IActionResult> Index()
        {
            var vehiculos = await _vehiculoRepository.GetAll();
            return View(vehiculos);
        }

        // GET: Vehiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var vehiculo = await _vehiculoRepository.GetById(id.Value);
            if (vehiculo == null) return NotFound();

            return View(vehiculo);
        }

        // GET: Vehiculoes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MarcaId"] = new SelectList(await _marcaRepository.GetAll(), "MarcaId", "Nombre");
            return View();
        }

        // POST: Vehiculoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                ViewData["MarcaId"] = new SelectList(await _marcaRepository.GetAll(), "MarcaId", "Nombre", vehiculo.MarcaId);
                return View(vehiculo);
            }

            await _vehiculoRepository.Add(vehiculo);
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var vehiculo = await _vehiculoRepository.GetById(id.Value);
            if (vehiculo == null) return NotFound();

            ViewData["MarcaId"] = new SelectList(await _marcaRepository.GetAll(), "MarcaId", "Nombre", vehiculo.MarcaId);
            return View(vehiculo);
        }

        // POST: Vehiculoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.VehiculoId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["MarcaId"] = new SelectList(await _marcaRepository.GetAll(), "MarcaId", "Nombre", vehiculo.MarcaId);
                return View(vehiculo);
            }

            try
            {
                await _vehiculoRepository.Update(vehiculo);
            }
            catch
            {
                if (!await _vehiculoRepository.Exists(vehiculo.VehiculoId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Vehiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var vehiculo = await _vehiculoRepository.GetById(id.Value);
            if (vehiculo == null) return NotFound();

            return View(vehiculo);
        }

        // POST: Vehiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vehiculoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
