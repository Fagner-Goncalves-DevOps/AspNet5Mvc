using AspNet5Mvc.Models;
using Microsoft.AspNetCore.Mvc;
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
        private static IList<Instituicao> _instituicaos = new List<Instituicao>()
        {
         new Instituicao(){InstituicaoID = 1, Nome="UniParaná", Endereco="Paraná"},
         new Instituicao(){InstituicaoID = 2, Nome="UniSanta", Endereco="Santa Catarina"},
         new Instituicao(){InstituicaoID = 3, Nome="UniSãoPaulo", Endereco="São Paulo"},
         new Instituicao(){InstituicaoID = 4, Nome="UniSulgrandense", Endereco="Rio Grande do Sul"},
         new Instituicao(){InstituicaoID = 5, Nome="UniCarioca", Endereco="Rio de Janeiro"}
        };

        /*
        action result ja eh uma classe abstrata, outras classes são estendidas
        quanto mais descer  o nível hierárquico das interfaces e classes, mais especializados ficam os tipos de retorno para as actions.
        existe tambem outras formas de retornos especializados 
        */
        
        public IActionResult Index() 
        {
            return View(_instituicaos.OrderBy(i =>i.Nome));
        }
 

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Instituicao instituicao)
        {
            _instituicaos.Add(instituicao);
            instituicao.InstituicaoID = _instituicaos.Select(i => i.InstituicaoID).Max() + 1;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            return View(_instituicaos.Where(i => i.InstituicaoID == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Instituicao instituicao) 
        {
            _instituicaos.Remove(_instituicaos.Where(i => i.InstituicaoID == instituicao.InstituicaoID).FirstOrDefault());
            _instituicaos.Add(instituicao);
            //_instituicaos[_instituicaos.IndexOf(_instituicaos.Where(i=> i.InstituicaoID == instituicao.InstituicaoID).FirstOrDefault())] = instituicao;
            //_instituicaos.Add(instituicao);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long id) 
        {
            var detalhes = _instituicaos.Where(i => i.InstituicaoID == id).FirstOrDefault();
            return View();
        }



        //deletando
        public ActionResult Delete(long id) 
        {
            return View(_instituicaos.Where(i => i.InstituicaoID == id).First()) ;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Instituicao instituicao) 
        {
            _instituicaos.Remove(_instituicaos.Where(i => i.InstituicaoID == instituicao.InstituicaoID).First());
            return RedirectToAction("Index");
        }



    }
}
