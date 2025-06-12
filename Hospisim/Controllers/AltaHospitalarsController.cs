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
    public class AltaHospitalarsController : Controller
    {
        private readonly HospisimContext _context;

        public AltaHospitalarsController(HospisimContext context)
        {
            _context = context;
        }

        // GET: AltaHospitalars
        public async Task<IActionResult> Index()
        {
            var hospisimContext = _context.AltasHospitalares.Include(a => a.Internacao);
            return View(await hospisimContext.ToListAsync());
        }

        // GET: AltaHospitalars/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .ThenInclude(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (altaHospitalar == null) return NotFound();

            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Create?internacaoId=...
        public async Task<IActionResult> Create(Guid internacaoId)
        {
            var internacao = await _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .FirstOrDefaultAsync(i => i.Id == internacaoId);

            if (internacao == null) return NotFound();

            ViewBag.PacienteNome = internacao.Paciente?.NomeCompleto;
            ViewBag.AtendimentoId = internacao.AtendimentoId;
            ViewBag.InternacaoId = internacao.Id;

            return View(new AltaHospitalar
            {
                InternacaoId = internacao.Id,
                Data = DateTime.Now
            });
        }

        // POST: AltaHospitalars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (ModelState.IsValid)
            {
                altaHospitalar.Id = Guid.NewGuid();

                var internacao = await _context.Internacoes
                    .Include(i => i.Atendimento)
                    .FirstOrDefaultAsync(i => i.Id == altaHospitalar.InternacaoId);

                if (internacao == null || internacao.Atendimento == null)
                    return NotFound();

                // Atualiza status da internação e atendimento
                internacao.StatusInternacao = StatusInternacao.AltaConcedida;
                internacao.Atendimento.Status = StatusAtendimento.Finalizado;

                _context.AltasHospitalares.Add(altaHospitalar);
                _context.Update(internacao);
                _context.Update(internacao.Atendimento);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Atendimentoes", new { id = internacao.AtendimentoId });
            }

            // Se cair aqui, precisa preencher as ViewBags novamente para reexibir a view corretamente
            var internacaoErro = await _context.Internacoes
                .Include(i => i.Paciente)
                .Include(i => i.Atendimento)
                .FirstOrDefaultAsync(i => i.Id == altaHospitalar.InternacaoId);

            if (internacaoErro != null)
            {
                ViewBag.PacienteNome = internacaoErro.Paciente?.NomeCompleto;
                ViewBag.AtendimentoId = internacaoErro.AtendimentoId;
                ViewBag.InternacaoId = internacaoErro.Id;
            }

            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar == null) return NotFound();

            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (id != altaHospitalar.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(altaHospitalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltaHospitalarExists(altaHospitalar.Id)) return NotFound();
                    else throw;
                }

                return RedirectToAction("Details", "Atendimentoes", new { id = altaHospitalar.Internacao?.AtendimentoId });
            }

            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .ThenInclude(i => i.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (altaHospitalar == null) return NotFound();

            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var alta = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .ThenInclude(i => i.Atendimento)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (alta != null)
            {
                // Reverte status de atendimento e internação
                if (alta.Internacao != null)
                {
                    alta.Internacao.StatusInternacao = StatusInternacao.Ativa;
                    if (alta.Internacao.Atendimento != null)
                        alta.Internacao.Atendimento.Status = StatusAtendimento.EmAndamento;

                    _context.Update(alta.Internacao);
                    _context.Update(alta.Internacao.Atendimento);
                }

                _context.AltasHospitalares.Remove(alta);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Atendimentoes", new { id = alta.Internacao.AtendimentoId });
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AltaHospitalarExists(Guid id)
        {
            return _context.AltasHospitalares.Any(e => e.Id == id);
        }
    }
}
