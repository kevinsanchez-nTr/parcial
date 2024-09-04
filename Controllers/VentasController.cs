using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using parcial.Models;
using parcial.Repository.Interface;

namespace parcial.Controllers
{
    public class VentasController : Controller
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IVehiculoRepository _vehiculoRepository;

        public VentasController(IVentaRepository ventaRepository, IVehiculoRepository vehiculoRepository)
        {
            _ventaRepository = ventaRepository;
            _vehiculoRepository = vehiculoRepository;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _ventaRepository.GetAll();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _ventaRepository.GetById(id.Value);
            if (venta == null) return NotFound();

            return View(venta);
        }

        // GET: Ventas/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.VehiculoId = new SelectList(await _vehiculoRepository.GetAll(), "VehiculoId", "Modelo");
            return View();
        }

        // POST: Ventas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta venta)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.VehiculoId = new SelectList(await _vehiculoRepository.GetAll(), "VehiculoId", "Modelo", venta.VehiculoId);
                return View(venta);
            }

            await _ventaRepository.Add(venta);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _ventaRepository.GetById(id.Value);
            if (venta == null) return NotFound();

            ViewBag.VehiculoId = new SelectList(await _vehiculoRepository.GetAll(), "VehiculoId", "Modelo", venta.VehiculoId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venta venta)
        {
            if (id != venta.VentaId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.VehiculoId = new SelectList(await _vehiculoRepository.GetAll(), "VehiculoId", "Modelo", venta.VehiculoId);
                return View(venta);
            }

            try
            {
                await _ventaRepository.Update(venta);
            }
            catch
            {
                if (!await _ventaRepository.Exists(venta.VentaId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var venta = await _ventaRepository.GetById(id.Value);
            if (venta == null) return NotFound();

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ventaRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
