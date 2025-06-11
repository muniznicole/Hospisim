using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class PrescricaosController : Controller
    {
        private readonly HospisimContext _context;

        public PrescricaosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Prescricaos/Create?atendimentoId=...
        public IActionResult Create(Guid atendimentoId)
        {
            var atendimento = _context.Atendimentos
                .Include(a => a.Profissional)
                .FirstOrDefault(a => a.Id == atendimentoId);

            if (atendimento == null)
                return NotFound();

            ViewBag.AtendimentoId = atendimento.Id;
            ViewBag.ProfissionalId = atendimento.ProfissionalId;
            ViewBag.ProfissionalNome = atendimento.Profissional.NomeCompleto;

            var prescricao = new Prescricao
            {
                AtendimentoId = atendimento.Id,
                ProfissionalId = atendimento.ProfissionalId,
                DataInicio = DateTime.Now,
                StatusPrescricao = Models.Enums.StatusPrescricao.Ativa
            };

            return View(prescricao);
        }

        // POST: Prescricaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prescricao prescricao)
        {
            if (ModelState.IsValid)
            {
                prescricao.Id = Guid.NewGuid();
                _context.Add(prescricao);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentoes", new { id = prescricao.AtendimentoId });
            }

            var atendimento = await _context.Atendimentos
                .Include(a => a.Profissional)
                .FirstOrDefaultAsync(a => a.Id == prescricao.AtendimentoId);

            ViewBag.AtendimentoId = atendimento?.Id;
            ViewBag.ProfissionalId = atendimento?.ProfissionalId;
            ViewBag.ProfissionalNome = atendimento?.Profissional?.NomeCompleto;

            return View(prescricao);
        }

        // GET: Prescricaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var prescricao = await _context.Prescricoes.FindAsync(id);
            if (prescricao == null)
                return NotFound();

            return View(prescricao);
        }

        // POST: Prescricaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Prescricao prescricao)
        {
            if (id != prescricao.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescricaoExists(prescricao.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction("Details", "Atendimentoes", new { id = prescricao.AtendimentoId });
            }

            return View(prescricao);
        }

        // GET: Prescricaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var prescricao = await _context.Prescricoes
                .Include(p => p.Atendimento)
                .Include(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);

            return prescricao == null ? NotFound() : View(prescricao);
        }

        // POST: Prescricaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prescricao = await _context.Prescricoes.FindAsync(id);
            var atendimentoId = prescricao?.AtendimentoId;

            if (prescricao != null)
            {
                _context.Prescricoes.Remove(prescricao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Atendimentoes", new { id = atendimentoId });
        }

        private bool PrescricaoExists(Guid id)
        {
            return _context.Prescricoes.Any(e => e.Id == id);
        }
    }
}
