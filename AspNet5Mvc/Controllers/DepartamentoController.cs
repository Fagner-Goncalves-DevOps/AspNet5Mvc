using AspNet5Mvc.Data;
using AspNet5Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly SqlContext _sqlContext;

        public DepartamentoController(SqlContext sqlContext) 
        {
            _sqlContext = sqlContext;
        }

        public async Task<IActionResult> Index() 
        {
            return View(await _sqlContext.Departamentos.Include(d => d.Instituicao).OrderBy(c => c.Nome).ToListAsync());
        }

        //criar
        public IActionResult Create() 
        {
            var instituicoes = _sqlContext.Instituicoes.OrderBy(c => c.Nome).ToList();
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome= "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, InstituicaoID")] Departamento departamento) 
        {
            //tentar entender depois !ModelState.IsValid sem try
            try
            {
                if (ModelState.IsValid)
                {
                    _sqlContext.Add(departamento);
                    await _sqlContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
    
            }
            return View(departamento);
        }

        public async Task<IActionResult> Edit(long? id) 
        {
            if (id == null) return NotFound();
            var departamento = await _sqlContext.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            if (departamento == null) return NotFound();
            ViewBag.Instituicoes = new SelectList(_sqlContext.Instituicoes.OrderBy(c=>c.Nome), "InstituicaoID", "Nome", departamento.DepartamentoID );
            return View(departamento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID, Nome, InstituicaoID")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID) return NotFound();
            if(ModelState.IsValid)
                try 
                {
                    _sqlContext.Update(departamento);
                    await _sqlContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException) 
                {
                    if (!DepartamentoExists(departamento.DepartamentoID)) return NotFound();
                }
            ViewBag.Instituicoes = new SelectList(_sqlContext.Instituicoes.OrderBy(c=>c.Nome), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return View(departamento);

        }

        public async Task<IActionResult> Details(long? id) 
        {
            if (id == null) return NotFound();
            var departamento = await _sqlContext.Departamentos.SingleOrDefaultAsync(d =>d.DepartamentoID==id);
            _sqlContext.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
            if (departamento == null) return NotFound();
            return View(departamento);

        }

        public async Task<IActionResult> Delete(long? id) 
        {
            if (id == null) return NotFound();
            var departamento = await _sqlContext.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            //_sqlContext.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
            if (departamento == null) return NotFound();
            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id) 
        {
            var departamento = await _sqlContext.Departamentos.SingleOrDefaultAsync(d => d.DepartamentoID == id);
            _sqlContext.Departamentos.Remove(departamento);
            await _sqlContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(long? id) 
        {
            return _sqlContext.Departamentos.Any(e => e.DepartamentoID == id);
        }


    }
}
