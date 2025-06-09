using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class ProfissionalsController : Controller
    {
        private readonly HospisimContext _context;

        public ProfissionalsController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Profissionals
        public async Task<IActionResult> Index()
        {
            var hospisimContext = _context.Profissionais.Include(p => p.Especialidade);
            return View(await hospisimContext.ToListAsync());
        }

        // GET: Profissionals/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .Include(p => p.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // GET: Profissionals/Create
        public IActionResult Create()
        {
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome");
            return View();
        }

        // POST: Profissionals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCompleto,CPF,Email,Telefone,RegistroConselho,TipoRegistro,EspecialidadeId,DataAdmissao,CargaHorariaSemanal,Turno,Ativo")] Profissional profissional)
        {
            if (ModelState.IsValid)
            {
                profissional.Id = Guid.NewGuid();
                _context.Add(profissional);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissional.EspecialidadeId);
            return View(profissional);
        }

        // GET: Profissionals/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional == null)
            {
                return NotFound();
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissional.EspecialidadeId);
            return View(profissional);
        }

        // POST: Profissionals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NomeCompleto,CPF,Email,Telefone,RegistroConselho,TipoRegistro,EspecialidadeId,DataAdmissao,CargaHorariaSemanal,Turno,Ativo")] Profissional profissional)
        {
            if (id != profissional.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profissional);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfissionalExists(profissional.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EspecialidadeId"] = new SelectList(_context.Especialidades, "Id", "Nome", profissional.EspecialidadeId);
            return View(profissional);
        }

        // GET: Profissionals/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profissional = await _context.Profissionais
                .Include(p => p.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profissional == null)
            {
                return NotFound();
            }

            return View(profissional);
        }

        // POST: Profissionals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var profissional = await _context.Profissionais.FindAsync(id);
            if (profissional != null)
            {
                _context.Profissionais.Remove(profissional);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfissionalExists(Guid id)
        {
            return _context.Profissionais.Any(e => e.Id == id);
        }
    }
}
