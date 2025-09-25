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
    public class DicaInvestimentoModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DicaInvestimentoModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DicaInvestimentoModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dicas.ToListAsync());
        }

        // GET: DicaInvestimentoModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dicaInvestimentoModel = await _context.Dicas
                .FirstOrDefaultAsync(m => m.idDica == id);
            if (dicaInvestimentoModel == null)
            {
                return NotFound();
            }

            return View(dicaInvestimentoModel);
        }

        // GET: DicaInvestimentoModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DicaInvestimentoModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("idDica,titulo,descricao,categoria,dataPublicacao,link")] DicaInvestimentoModel dicaInvestimentoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dicaInvestimentoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dicaInvestimentoModel);
        }

        // GET: DicaInvestimentoModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dicaInvestimentoModel = await _context.Dicas.FindAsync(id);
            if (dicaInvestimentoModel == null)
            {
                return NotFound();
            }
            return View(dicaInvestimentoModel);
        }

        // POST: DicaInvestimentoModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("idDica,titulo,descricao,categoria,dataPublicacao,link")] DicaInvestimentoModel dicaInvestimentoModel)
        {
            if (id != dicaInvestimentoModel.idDica)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dicaInvestimentoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DicaInvestimentoModelExists(dicaInvestimentoModel.idDica))
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
            return View(dicaInvestimentoModel);
        }

        // GET: DicaInvestimentoModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dicaInvestimentoModel = await _context.Dicas
                .FirstOrDefaultAsync(m => m.idDica == id);
            if (dicaInvestimentoModel == null)
            {
                return NotFound();
            }

            return View(dicaInvestimentoModel);
        }

        // POST: DicaInvestimentoModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dicaInvestimentoModel = await _context.Dicas.FindAsync(id);
            if (dicaInvestimentoModel != null)
            {
                _context.Dicas.Remove(dicaInvestimentoModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DicaInvestimentoModelExists(int id)
        {
            return _context.Dicas.Any(e => e.idDica == id);
        }
    }
}
