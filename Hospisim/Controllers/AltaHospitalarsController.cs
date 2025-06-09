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
            if (id == null)
            {
                return NotFound();
            }

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (altaHospitalar == null)
            {
                return NotFound();
            }

            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Create
        public IActionResult Create()
        {
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id");
            return View();
        }

        // POST: AltaHospitalars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (ModelState.IsValid)
            {
                altaHospitalar.Id = Guid.NewGuid();
                _context.Add(altaHospitalar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id", altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar == null)
            {
                return NotFound();
            }
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id", altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,InternacaoId,Data,CondicaoPaciente,InstrucoesPosAlta")] AltaHospitalar altaHospitalar)
        {
            if (id != altaHospitalar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(altaHospitalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AltaHospitalarExists(altaHospitalar.Id))
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
            ViewData["InternacaoId"] = new SelectList(_context.Internacoes, "Id", "Id", altaHospitalar.InternacaoId);
            return View(altaHospitalar);
        }

        // GET: AltaHospitalars/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var altaHospitalar = await _context.AltasHospitalares
                .Include(a => a.Internacao)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (altaHospitalar == null)
            {
                return NotFound();
            }

            return View(altaHospitalar);
        }

        // POST: AltaHospitalars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var altaHospitalar = await _context.AltasHospitalares.FindAsync(id);
            if (altaHospitalar != null)
            {
                _context.AltasHospitalares.Remove(altaHospitalar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AltaHospitalarExists(Guid id)
        {
            return _context.AltasHospitalares.Any(e => e.Id == id);
        }
    }
}
