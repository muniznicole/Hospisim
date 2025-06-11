using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospisim.Data;
using Hospisim.Models;

namespace Hospisim.Controllers
{
    public class AtendimentoesController : Controller
    {
        private readonly HospisimContext _context;

        public AtendimentoesController(HospisimContext context)
        {
            _context = context;
        }

        // GET: Atendimentoes/PorProntuario
        public async Task<IActionResult> PorProntuario(Guid prontuarioId)
        {
            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(p => p.Id == prontuarioId);

            if (prontuario == null) return NotFound();

            ViewBag.ProntuarioId = prontuario.Id;
            ViewBag.PacienteId = prontuario.PacienteId;
            ViewBag.PacienteNome = prontuario.Paciente.NomeCompleto;
            ViewBag.ProntuarioNumero = prontuario.Numero;

            var atendimentos = await _context.Atendimentos
                .Include(a => a.Profissional)
                    .ThenInclude(p => p.Especialidade)
                .Where(a => a.ProntuarioId == prontuarioId)
                .ToListAsync();

            return View("Index", atendimentos);
        }

        // GET: Atendimentoes/Create
        public IActionResult Create(Guid prontuarioId)
        {
            var prontuario = _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefault(p => p.Id == prontuarioId);

            if (prontuario == null) return NotFound();

            ViewBag.ProntuarioId = prontuario.Id;
            ViewBag.PacienteNome = prontuario.Paciente.NomeCompleto;
            ViewBag.ProntuarioNumero = prontuario.Numero;
            ViewBag.ProfissionalId = new SelectList(
                _context.Profissionais
                    .Include(p => p.Especialidade)
                    .Select(p => new {
                        p.Id,
                        NomeCompleto = p.NomeCompleto + " - " + p.Especialidade.Nome
                    }).ToList(),
                "Id", "NomeCompleto"
            );

            var atendimento = new Atendimento
            {
                ProntuarioId = prontuarioId,
                DataHora = DateTime.Now,
                Status = StatusAtendimento.EmAndamento
            };

            return View(atendimento);
        }

        // POST: Atendimentoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataHora,Tipo,Status,Local,ProntuarioId,ProfissionalId")] Atendimento atendimento)
        {
            if (ModelState.IsValid)
            {
                atendimento.Id = Guid.NewGuid();
                _context.Atendimentos.Add(atendimento);
                await _context.SaveChangesAsync();
                return RedirectToAction("PorProntuario", new { prontuarioId = atendimento.ProntuarioId });
            }

            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente)
                .FirstOrDefaultAsync(p => p.Id == atendimento.ProntuarioId);

            ViewBag.ProntuarioId = atendimento.ProntuarioId;
            ViewBag.PacienteNome = prontuario?.Paciente?.NomeCompleto ?? "Paciente";
            ViewBag.ProntuarioNumero = prontuario?.Numero ?? "N/A";
            ViewBag.ProfissionalId = new SelectList(
                _context.Profissionais
                    .Include(p => p.Especialidade)
                    .Select(p => new {
                        p.Id,
                        NomeCompleto = p.NomeCompleto + " - " + p.Especialidade.Nome
                    }).ToList(),
                "Id", "NomeCompleto",
                atendimento.ProfissionalId
            );

            return View(atendimento);
        }

        // GET: Atendimentoes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var atendimento = await _context.Atendimentos
                .Include(a => a.Prontuario)
                    .ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (atendimento == null) return NotFound();

            ViewBag.ProfissionalId = new SelectList(
                _context.Profissionais.Include(p => p.Especialidade),
                "Id", "NomeCompleto", atendimento.ProfissionalId);

            // ✅ Preencher nome e número do prontuário para exibir no topo da view
            ViewBag.PacienteNome = atendimento.Prontuario?.Paciente?.NomeCompleto ?? "Paciente";
            ViewBag.ProntuarioNumero = atendimento.Prontuario?.Numero ?? "N/A";
            ViewBag.ProntuarioId = atendimento.ProntuarioId;

            return View(atendimento);
        }

        // POST: Atendimentoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,DataHora,Tipo,Status,Local,ProntuarioId,ProfissionalId")] Atendimento atendimento)
        {
            if (id != atendimento.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(atendimento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AtendimentoExists(atendimento.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction("PorProntuario", new { prontuarioId = atendimento.ProntuarioId });
            }

            ViewBag.ProfissionalId = new SelectList(
                _context.Profissionais
                    .Include(p => p.Especialidade)
                    .Select(p => new {
                        p.Id,
                        NomeCompleto = p.NomeCompleto + " - " + p.Especialidade.Nome
                    }).ToList(),
                "Id", "NomeCompleto",
                atendimento.ProfissionalId
            );

            return View(atendimento);
        }

        // GET: Atendimentoes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var atendimento = await _context.Atendimentos
                .Include(a => a.Profissional)
                    .ThenInclude(p => p.Especialidade)
                .Include(a => a.Prontuario)
                    .ThenInclude(p => p.Paciente)
                .Include(a => a.Prescricoes)
                    .ThenInclude(p => p.Profissional)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (atendimento == null) return NotFound();

            ViewBag.PacienteNome = atendimento.Prontuario?.Paciente?.NomeCompleto ?? "Paciente";
            ViewBag.ProntuarioNumero = atendimento.Prontuario?.Numero ?? "N/A";

            return View(atendimento);
        }


        // GET: Atendimentoes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var atendimento = await _context.Atendimentos
                .Include(a => a.Profissional)
                    .ThenInclude(p => p.Especialidade)
                .Include(a => a.Prontuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            return atendimento == null ? NotFound() : View(atendimento);
        }

        // POST: Atendimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var atendimento = await _context.Atendimentos.FindAsync(id);
            if (atendimento != null)
            {
                _context.Atendimentos.Remove(atendimento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("PorProntuario", new { prontuarioId = atendimento.ProntuarioId });
        }

        private bool AtendimentoExists(Guid id)
        {
            return _context.Atendimentos.Any(e => e.Id == id);
        }
    }
}
