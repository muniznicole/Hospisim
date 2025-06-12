using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;
using Hospisim.Models.Enums;

namespace Hospisim.Controllers
{
    public class ExamesController : Controller
    {
        private readonly HospisimContext _context;

        public ExamesController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Exames/Create?atendimentoId=...
        public IActionResult Create(Guid atendimentoId)
        {
            var atendimento = _context.Atendimentos
                .Include(a => a.Profissional)
                .FirstOrDefault(a => a.Id == atendimentoId);

            if (atendimento == null)
                return NotFound();

            ViewBag.AtendimentoId = atendimento.Id;
            ViewBag.ProfissionalNome = atendimento.Profissional?.NomeCompleto;

            var exame = new Exame
            {
                AtendimentoId = atendimento.Id,
                DataSolicitacao = DateTime.Now,
                StatusExame = StatusExame.Solicitado
            };

            return View(exame);
        }

        // POST: Exames/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Exame exame)
        {
            if (ModelState.IsValid)
            {
                exame.Id = Guid.NewGuid();
                _context.Add(exame);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Atendimentoes", new { id = exame.AtendimentoId });
            }

            ViewBag.AtendimentoId = exame.AtendimentoId;

            return View(exame);
        }

        // GET: Exames/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var exame = await _context.Exames.FindAsync(id);
            if (exame == null) return NotFound();

            return View(exame);
        }

        // POST: Exames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Exame exame)
        {
            if (id != exame.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exame);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExameExists(exame.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction("Details", "Atendimentoes", new { id = exame.AtendimentoId });
            }

            return View(exame);
        }

        // GET: Exames/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var exame = await _context.Exames
                .Include(e => e.Atendimento)
                .FirstOrDefaultAsync(m => m.Id == id);

            return exame == null ? NotFound() : View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exame = await _context.Exames.FindAsync(id);
            var atendimentoId = exame?.AtendimentoId;

            if (exame != null)
            {
                _context.Exames.Remove(exame);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Atendimentoes", new { id = atendimentoId });
        }

        private bool ExameExists(Guid id)
        {
            return _context.Exames.Any(e => e.Id == id);
        }
    }
}
