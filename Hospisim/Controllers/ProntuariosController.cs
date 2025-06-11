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
    public class ProntuariosController : Controller
    {
        private readonly HospisimContext _context;

        public ProntuariosController(HospisimContext context)
        {
            _context = context;
        }

        // ✅ Lista geral (opcional)
        public async Task<IActionResult> Index()
        {
            var prontuarios = await _context.Prontuarios
                .Include(p => p.Paciente)
                .ToListAsync();
            return View(prontuarios);
        }

        // ✅ Lista filtrada por paciente (sem conflito de rota)
        [HttpGet]
        [ActionName("PorPaciente")]
        public async Task<IActionResult> PorPaciente(Guid pacienteId)
        {
            var paciente = await _context.Pacientes.FindAsync(pacienteId);
            if (paciente == null) return NotFound();

            ViewBag.PacienteNome = paciente.NomeCompleto;
            ViewBag.PacienteId = paciente.Id;

            var prontuarios = await _context.Prontuarios
                .Where(p => p.PacienteId == pacienteId)
                .ToListAsync();

            return View("PorPaciente", prontuarios);
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var prontuario = await _context.Prontuarios.Include(p => p.Paciente).FirstOrDefaultAsync(p => p.Id == id);
            return prontuario == null ? NotFound() : View(prontuario);
        }

        public IActionResult Create(Guid pacienteId)
        {
            var paciente = _context.Pacientes.Find(pacienteId);
            if (paciente == null) return NotFound();

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", pacienteId);
            ViewBag.PacienteNome = paciente.NomeCompleto;

            return View(new Prontuario { PacienteId = pacienteId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObservacoesGerais,PacienteId")] Prontuario prontuario)
        {
            if (ModelState.IsValid)
            {
                prontuario.Id = Guid.NewGuid();
                prontuario.DataAbertura = DateTime.Now;
                prontuario.Numero = $"PRT-{DateTime.Now:yyyyMMddHHmmss}";

                _context.Add(prontuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("PorPaciente", new { pacienteId = prontuario.PacienteId });
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            return View(prontuario);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var prontuario = await _context.Prontuarios
                .Include(p => p.Paciente) // necessário para acessar prontuario.Paciente.NomeCompleto
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prontuario == null) return NotFound();

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            ViewBag.PacienteNome = prontuario.Paciente?.NomeCompleto; // aqui 👈

            return View(prontuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Numero,DataAbertura,ObservacoesGerais,PacienteId")] Prontuario prontuario)
        {
            if (id != prontuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prontuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProntuarioExists(prontuario.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("PorPaciente", new { pacienteId = prontuario.PacienteId });
            }

            ViewData["PacienteId"] = new SelectList(_context.Pacientes, "Id", "NomeCompleto", prontuario.PacienteId);
            return View(prontuario);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var prontuario = await _context.Prontuarios.Include(p => p.Paciente).FirstOrDefaultAsync(p => p.Id == id);
            if (prontuario == null) return NotFound();

            ViewBag.PacienteNome = prontuario.Paciente?.NomeCompleto;
            return View(prontuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var prontuario = await _context.Prontuarios.FindAsync(id);
            if (prontuario != null)
            {
                _context.Prontuarios.Remove(prontuario);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProntuarioExists(Guid id)
        {
            return _context.Prontuarios.Any(e => e.Id == id);
        }
    }
}
