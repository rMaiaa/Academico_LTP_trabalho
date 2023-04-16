using Academico.Data;
using Academico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academico.Controllers
{
    public class DepartamentoController : Controller
    {
        public readonly AcademicoContext _context;

        public DepartamentoController(AcademicoContext context)
        {
            _context = context;
        }

        public bool DepartamentoExists(long? id)
        {
            return _context.Departamentos.Any(i => i.DepartamentoId == id);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Departamentos.OrderBy(i => i.Nome).ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create([Bind("Nome")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(departamento);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível cadastrar o departamento");
            }
            return View(departamento);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(long? id, [Bind("Nome")] Departamento departamento)
        {
            if (id != departamento.DepartamentoId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(departamento);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }


        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.DepartamentoId == id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await _context.Departamentos.SingleOrDefaultAsync(i => i.DepartamentoId == id);
            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
