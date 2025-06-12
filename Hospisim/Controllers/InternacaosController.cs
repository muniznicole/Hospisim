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
            var hospisimContext = _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente);
            return View(await hospisimContext.ToListAsync());
        }

        // GET: Internacaos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (internacao == null) return NotFound();

            return View(internacao);
        }

        // GET: Internacaos/Create?atendimentoId=...
        public async Task<IActionResult> Create(Guid atendimentoId)
        {
            var atendimento = await _context.Atendimentos
                .Include(a => a.Prontuario)
                    .ThenInclude(p => p.Paciente)
                .FirstOrDefaultAsync(a => a.Id == atendimentoId);

            if (atendimento == null) return NotFound();

            ViewBag.AtendimentoId = atendimento.Id;
            ViewBag.PacienteId = atendimento.Prontuario.PacienteId;
            ViewBag.PacienteNome = atendimento.Prontuario.Paciente.NomeCompleto;

            var internacao = new Internacao
            {
                AtendimentoId = atendimento.Id,
                PacienteId = atendimento.Prontuario.PacienteId,
                DataEntrada = DateTime.Now,
                StatusInternacao = StatusInternacao.Ativa
            };

            ViewBag.AtendimentoId = internacao.AtendimentoId;
            ViewBag.PacienteId = internacao.PacienteId;
            ViewBag.PacienteNome = atendimento.Prontuario.Paciente.NomeCompleto;

            return View(internacao);
        }

        // POST: Internacaos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas")] Internacao internacao)
        {
            if (ModelState.IsValid)
            {
                internacao.Id = Guid.NewGuid();
                internacao.StatusInternacao = StatusInternacao.Ativa;
                _context.Add(internacao);

                // 👉 Atualizar o tipo do atendimento para Internacao
                var atendimento = await _context.Atendimentos.FindAsync(internacao.AtendimentoId);
                if (atendimento != null)
                {
                    atendimento.Tipo = TipoAtendimento.Internacao;
                    _context.Entry(atendimento).Property(a => a.Tipo).IsModified = true;
                }
                
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Atendimentoes", new { id = internacao.AtendimentoId });
            }

            // Reenvio de dados para preencher campos da view em caso de erro
            ViewBag.AtendimentoId = internacao.AtendimentoId;
            ViewBag.PacienteId = internacao.PacienteId;
            ViewBag.PacienteNome = ""; // opcional: buscar novamente o nome se necessário

            return View(internacao);
        }

        // GET: Internacaos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao == null) return NotFound();

            return View(internacao);
        }

        // POST: Internacaos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PacienteId,AtendimentoId,DataEntrada,PrevisaoAlta,MotivoInternacao,Leito,Quarto,Setor,PlanoSaudeUtilizado,ObservacoesClinicas,StatusInternacao")] Internacao internacao)
        {
            if (id != internacao.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(internacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternacaoExists(internacao.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction("Details", "Atendimentoes", new { id = internacao.AtendimentoId });
            }

            return View(internacao);
        }

        // GET: Internacaos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var internacao = await _context.Internacoes
                .Include(i => i.Atendimento)
                .Include(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            return internacao == null ? NotFound() : View(internacao);
        }

        // POST: Internacaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var internacao = await _context.Internacoes.FindAsync(id);
            if (internacao != null)
            {
                // 🔄 Atualiza o atendimento para voltar a ser consulta
                var atendimento = await _context.Atendimentos.FindAsync(internacao.AtendimentoId);
                if (atendimento != null && atendimento.Tipo == TipoAtendimento.Internacao)
                {
                    atendimento.Tipo = TipoAtendimento.Consulta;
                    _context.Entry(atendimento).Property(a => a.Tipo).IsModified = true;
                }

                _context.Internacoes.Remove(internacao);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", "Atendimentoes", new { id = internacao.AtendimentoId });
        }

        private bool InternacaoExists(Guid id)
        {
            return _context.Internacoes.Any(e => e.Id == id);
        }
    }
}
