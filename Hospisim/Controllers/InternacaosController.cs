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
    public class InternacaosController : Controller
    {
        private readonly HospisimContext _context;

        public InternacaosController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Internacaos
        public async Task<IActionResult> Index()
        {
            var hospisimContext = _context.Internacoes.Include(i => i.Atendimento).Include(i => i.Paciente);
            return View(await hospisimContext.ToListAsync());
        }

        // GET: Internacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null)
            {
                return NotFound();
            }

            return View(internacao);
        }

        // GET: Internacaos/Create
        public IActionResult Create()
        {
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id");
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Id");
            return View();
        }

        // POST: Internacaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (ModelState.IsValid)
            {
                internacao.Id = Guid.NewGuid();
                _context.Add(internacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Id", internacao.PacienteId);
            return View(internacao);
        }

        // GET: Internacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao == null)
            {
                return NotFound();
            }
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Id", internacao.PacienteId);
            return View(internacao);
        }

        // POST: Internacaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (id != internacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternacaoExists(internacao.Id))
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
            ViewData["AtendimentoId"] = new SelectList(_context.Atendimentos, "Id", "Id", internacao.AtendimentoId);
            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "Id", internacao.PacienteId);
            return View(internacao);
        }

        // GET: Internacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (internacao == null)
            {
                return NotFound();
            }

            return View(internacao);
        }

        // POST: Internacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao != null)
            {
                _context.Internacoes.Remove(internacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InternacaoExists(Guid id)
        {
            return _context.Internacoes.Any(e => e.Id == id);
        }
    }
}
