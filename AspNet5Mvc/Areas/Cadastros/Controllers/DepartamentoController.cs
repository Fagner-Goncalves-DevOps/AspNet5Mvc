using AspNet5Mvc.Data;
using AspNet5Mvc.Data.Repository.Cadastros;
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

namespace AspNet5Mvc.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    public class DepartamentoController : Controller
    {
        //não esta usando injeção dependencia
        private readonly SqlContext _sqlContext;
        private readonly DepartamentoRepository departamentoRepository;
        private readonly InstituicaoRepository instituicaoRepository;

        public DepartamentoController(SqlContext sqlContext)
        {
            _sqlContext = sqlContext;
            departamentoRepository = new DepartamentoRepository(sqlContext);
            instituicaoRepository = new InstituicaoRepository(sqlContext);
        }

        public async Task<IActionResult> Index()
        {
            return View(await departamentoRepository.ObterDepartamentosClassificadosPorNome().ToListAsync());
        }

        //criar
        public IActionResult Create()
        {
            var instituicoes = instituicaoRepository.ObterInstituicoesOrderPorNome().ToList();
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione	a instituição" });
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
                    await departamentoRepository.GravarDepartamento(departamento);
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
            ViewResult visaoDepartamento = (ViewResult)await ObterVisaoDepartamentoPorId(id);
            Departamento departamento = (Departamento)visaoDepartamento.Model;
            ViewBag.Instituicoes = new SelectList(instituicaoRepository.ObterInstituicoesOrderPorNome(), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return visaoDepartamento;

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("DepartamentoID, Nome, InstituicaoID")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID) return NotFound();
            if (ModelState.IsValid)
                try
                {
                    await departamentoRepository.GravarDepartamento(departamento);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await DepartamentoExists(departamento.DepartamentoID)) return NotFound();
                }
            ViewBag.Instituicoes = new SelectList(instituicaoRepository.ObterInstituicoesOrderPorNome(), "InstituicaoID", "Nome", departamento.InstituicaoID);
            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var departamento = await departamentoRepository.EliminarDepartamentoPorId((long)id);
            TempData["Message"] = "Departamento	" + departamento.Nome.ToUpper() + "	foi	removido";
            return RedirectToAction(nameof(Index));
        }




        //Metodos unicos gerados para melhoria do codigo
        private async Task<bool> DepartamentoExists(long? id)
        {
            return await departamentoRepository.ObterDepartamentoPorId((long)id) != null;
        }
        public async Task<IActionResult> Details(long? id)
        {
            return await ObterVisaoDepartamentoPorId(id);
        }
        public async Task<IActionResult> Delete(long? id)
        {
            return await ObterVisaoDepartamentoPorId(id);
        }



        private async Task<IActionResult> ObterVisaoDepartamentoPorId(long? id)
        {
            if (id == null) return NotFound();
            var departamento = await departamentoRepository.ObterDepartamentoPorId((long)id);
            if (departamento == null) return NotFound();
            return View(departamento);


        }
    }
}