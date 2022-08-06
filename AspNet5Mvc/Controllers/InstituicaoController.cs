using AspNet5Mvc.Data;
using AspNet5Mvc.Data.Repository.Cadastros;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Controllers
{
    public class InstituicaoController : Controller
    {
        //não esta usando injeção dependencia

        private readonly SqlContext _sqlContext;
        private readonly InstituicaoRepository _instituicaoRepository;

        public InstituicaoController(SqlContext sqlContext) 
        {
            _sqlContext = sqlContext; //metodos ainda estão direto no data
            _instituicaoRepository = new InstituicaoRepository(sqlContext);
        }
        /*
        ActionResult ja é uma classe abstrata, outras classes são estendidas
        quanto mais descer o nível hierárquico das interfaces e classes, mais especializados ficam os tipos de retorno para as actions.
        existe tambem outras formas de retornos especializados 
        */
        public async Task<IActionResult> Index() 
        {
            return View(await _instituicaoRepository.ObterInstituicoesOrderPorNome().ToListAsync());
        }


        //Criar novo
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, Endereco")] Instituicao instituicao)
        {
            try 
            {
                if (ModelState.IsValid) 
                {
                    await _instituicaoRepository.GravarInstituicao(instituicao);
                    return RedirectToAction(nameof(Index));
                }
            } 
            catch (DbUpdateException) 
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            } 
            return View(instituicao);
        }


        //Editar
        public async Task<IActionResult> Edit(long? id)
        {
            return await ObterVisaoInstituicaoPorId(id);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("InstituicaoID, Nome, Endereco")] Instituicao instituicao) 
        {
            if (id != instituicao.InstituicaoID) return NotFound();
            if (ModelState.IsValid)
            try 
            {
                await _instituicaoRepository.GravarInstituicao(instituicao);
                return RedirectToAction(nameof(Index));
            } 
            catch (DbUpdateConcurrencyException) 
            {
                if (! await InstituicaoExists(instituicao.InstituicaoID)) return NotFound();
            } 
            return View(instituicao);
        }

        //Detalhes
        public async Task<IActionResult> Details(long? id) 
        {
            return await ObterVisaoInstituicaoPorId(id);
        }

        //Deletar
        public async Task<IActionResult> Delete(long? id) 
        {
            return await ObterVisaoInstituicaoPorId(id);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            var instituicao = await _instituicaoRepository.EliminarInstituicaoPorId(id);
            TempData["Message"] = "Instituição" + instituicao.Nome.ToUpper() + "foi removida";
            return RedirectToAction(nameof(Index));
        }


        //Metodos unicos gerados para melhoria do codigo
        private async Task<bool> InstituicaoExists(long? id)
        {
            return await _instituicaoRepository.ObterInstituicaoPorId(id) != null;
        }

        private async Task<IActionResult> ObterVisaoInstituicaoPorId(long? id) 
        {
            if (id == null) return NotFound();
            var instituicao = await _instituicaoRepository.ObterInstituicaoPorId(id);
            if (instituicao == null) return NotFound();
            return View(instituicao);

        }
    }
}
