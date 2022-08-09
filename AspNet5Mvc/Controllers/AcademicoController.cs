using AspNet5Mvc.Data;
using AspNet5Mvc.Data.Repository.Cadastros;
using AspNet5Mvc.Data.Repository.Discente;
using Core.Entities;
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
    public class AcademicoController : Controller
    {
        //não esta usando injeção dependencia
        private readonly SqlContext _sqlContext;
        private readonly AcademicoRepository _academicoRepository;

        public AcademicoController(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
            _academicoRepository = new AcademicoRepository(sqlContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _academicoRepository.ObterAcademicosClassificadosPorNome().ToListAsync());
        }

        //	GET: Academico/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,RegistroAcademico,Nascimento")]	Academico academico)
        {
            try
            {
                if (ModelState.IsValid) await _academicoRepository.GravarAcademico(academico);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(academico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("AcademicoID,Nome,RegistroAcademico,Nascimento")]	Academico academico)
        {
            if (id != academico.AcademicoID) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await _academicoRepository.GravarAcademico(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(academico);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var academico = await _academicoRepository.EliminarAcademicoPorId((long)id);
            TempData["Message"] = "Acadêmico" + academico.Nome.ToUpper() + "foi	removida";
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> AcademicoExists(long? id)
        {
            return await _academicoRepository.ObterAcademicoPorId((long)id) != null;
        }

        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoAcademicoPorId(id);
        }

        //metodos para chamar
        private async Task<IActionResult> ObterVisaoAcademicoPorId(long? id)
        {
            if (id == null) return NotFound();
            var academico = await _academicoRepository.ObterAcademicoPorId((long)id);
            if (academico == null) return NotFound();

            return View(academico);
        }
    }
}