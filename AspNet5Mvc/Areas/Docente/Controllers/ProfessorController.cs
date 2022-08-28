using AspNet5Mvc.Areas.Docente.Models;
using AspNet5Mvc.Data;
using AspNet5Mvc.Data.Repository.Cadastros;
using AspNet5Mvc.Data.Repository.Docente;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Areas.Docente.Controllers
{
    [Area("Docente")]
    public class ProfessorController : Controller
    {
        private readonly SqlContext _context;
        private readonly InstituicaoRepository instituicaoRepository;
        private readonly DepartamentoRepository departamentoRepository;
        private readonly CursoRepository cursoRepository;
        private readonly ProfessorRepository professorRepository;

        public ProfessorController(SqlContext context)
        {
            _context = context;
            instituicaoRepository = new InstituicaoRepository(context);
            departamentoRepository = new DepartamentoRepository(context);
            cursoRepository = new CursoRepository(context);
            professorRepository = new ProfessorRepository(context);
        }

        public IActionResult AdicionarProfessor()
        {
            PrepararViewBags(instituicaoRepository.ObterInstituicoesOrderPorNome().ToList(),
                new List<Departamento>().ToList(), new List<Curso>().ToList(), new List<Professor>().ToList());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdicionarProfessor([Bind("InstituicaoID, DepartamentoID, CursoID, ProfessorID")] AdicionarProfessorViewModel model)
        {
            if (model.InstituicaoID == 0  ||
                model.DepartamentoID == 0 || 
                model.CursoID == 0 || 
                model.ProfessorID == 0
                )
            {
                ModelState.AddModelError("", "É preciso selecionar todos os dados");
            }
            else
            {
                cursoRepository.RegistrarProfessor((long)model.CursoID, (long)model.ProfessorID);

                RegistrarProfessorNaSessao((long)model.CursoID, (long)model.ProfessorID);

                PrepararViewBags(instituicaoRepository.ObterInstituicoesOrderPorNome().ToList(),
                    departamentoRepository.ObterDepartamentoPorInstituicao((long)model.InstituicaoID).ToList(),
                    cursoRepository.ObterCursosPorDepartamento((long)model.DepartamentoID).ToList(),
                    cursoRepository.ObterProfessoresForaDoCurso((long)model.CursoID).ToList());
            }
            return View(model);
        }

        public void RegistrarProfessorNaSessao(long cursoID, long professorID)
        {
            var cursoProfessor = new CursoProfessor() { ProfessorID = professorID, CursoID = cursoID };
            List<CursoProfessor> cursosProfessor = new List<CursoProfessor>();
            string cursosProfessoresSession = HttpContext.Session.GetString("cursosProfessores");
            if (cursosProfessoresSession != null)
            {
                cursosProfessor = JsonConvert.DeserializeObject<List<CursoProfessor>>(cursosProfessoresSession);
            }
            cursosProfessor.Add(cursoProfessor);
            HttpContext.Session.SetString("cursosProfessores", JsonConvert.SerializeObject(cursosProfessor));
        }

        public IActionResult VerificarUltimosRegistros()
        {
            List<CursoProfessor> cursosProfessor = new List<CursoProfessor>();
            string cursosProfessoresSession = HttpContext.Session.GetString("cursosProfessores");
            if (cursosProfessoresSession != null)
            {
                cursosProfessor = JsonConvert.DeserializeObject<List<CursoProfessor>>(cursosProfessoresSession);
            }
            return View(cursosProfessor);
        }

        public void PrepararViewBags(List<Instituicao> instituicoes, List<Departamento> departamentos, List<Curso> cursos, List<Professor> professores)
        {
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;

            departamentos.Insert(0, new Departamento() { DepartamentoID = 0, Nome = "Selecione o departamento" });
            ViewBag.Departamentos = departamentos;

            cursos.Insert(0, new Curso() { CursoID = 0, Nome = "Selecione o curso" });
            ViewBag.Cursos = cursos;

            professores.Insert(0, new Professor() { ProfessorID = 0, Nome = "Selecione o professor" });
            ViewBag.Professores = professores;
        }



        //retorno jsonresult para carregar filtros dos dropdown
        public JsonResult ObterDepartamentosPorInstituicao(long actionID)
        {
            var departamentos = departamentoRepository.ObterDepartamentoPorInstituicao(actionID).ToList();
            return Json(new SelectList(departamentos, "DepartamentoID", "Nome"));
        }

        public JsonResult ObterCursosPorDepartamento(long actionID)
        {
            var cursos = cursoRepository.ObterCursosPorDepartamento(actionID).ToList();
            return Json(new SelectList(cursos, "CursoID", "Nome"));
        }

        public JsonResult ObterProfessoresForaDoCurso(long actionID)
        {
            var professores = cursoRepository.ObterProfessoresForaDoCurso(actionID).ToList();
            return Json(new SelectList(professores, "ProfessorID", "Nome"));
        }
    }
}
