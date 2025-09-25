using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SprintData;
using SprintModel;

namespace SprintUI2.Controllers
{
    public class InvestimentoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvestimentoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvestimentoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Investimentos.ToListAsync());
        }

        // GET: InvestimentoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimentoModel = await _context.Investimentos
                .FirstOrDefaultAsync(m => m.idInvestimento == id);
            if (investimentoModel == null)
            {
                return NotFound();
            }

            return View(investimentoModel);
        }

        // GET: InvestimentoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvestimentoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idInvestimento,nome,tipo,rentabilidadeAnual,risco,valorMinimo")] InvestimentoModel investimentoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(investimentoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(investimentoModel);
        }

        // GET: InvestimentoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimentoModel = await _context.Investimentos.FindAsync(id);
            if (investimentoModel == null)
            {
                return NotFound();
            }
            return View(investimentoModel);
        }

        // POST: InvestimentoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idInvestimento,nome,tipo,rentabilidadeAnual,risco,valorMinimo")] InvestimentoModel investimentoModel)
        {
            if (id != investimentoModel.idInvestimento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investimentoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestimentoModelExists(investimentoModel.idInvestimento))
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
            return View(investimentoModel);
        }

        // GET: InvestimentoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var investimentoModel = await _context.Investimentos
                .FirstOrDefaultAsync(m => m.idInvestimento == id);
            if (investimentoModel == null)
            {
                return NotFound();
            }

            return View(investimentoModel);
        }

        // POST: InvestimentoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investimentoModel = await _context.Investimentos.FindAsync(id);
            if (investimentoModel != null)
            {
                _context.Investimentos.Remove(investimentoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvestimentoModelExists(int id)
        {
            return _context.Investimentos.Any(e => e.idInvestimento == id);
        }
    }
}
