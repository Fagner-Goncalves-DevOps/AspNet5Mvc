using AspNet5Mvc.Data;
using AspNet5Mvc.Data.Repository.Cadastros;
using AspNet5Mvc.Data.Repository.Discente;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Mvc.Areas.Discente.Controllers
{
    [Area("Discente")]
    public class AcademicoController : Controller
    {
        //não esta usando injeção dependencia
        private readonly SqlContext _sqlContext;
        private readonly AcademicoRepository _academicoRepository;
        private IWebHostEnvironment _env;

        public AcademicoController(SqlContext sqlContext, IWebHostEnvironment env)
        {
            _sqlContext = sqlContext;
            _academicoRepository = new AcademicoRepository(sqlContext);
            _env = env;
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
        public async Task<IActionResult> Edit(long? id, [Bind("AcademicoID,Nome,RegistroAcademico,Nascimento")] Academico academico, IFormFile foto, string chkRemoverFoto)
        {
            if (id != academico.AcademicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var stream = new MemoryStream();
                    if (chkRemoverFoto != null)
                    {
                        academico.Foto = null;
                    }
                    else
                    {
                        await foto.CopyToAsync(stream);
                        academico.Foto = stream.ToArray();
                        academico.FotoMimeType = foto.ContentType;
                    }

                    await _academicoRepository.GravarAcademico(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID))
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


        public async Task<FileContentResult> GetFoto(long id) 
        {
            Academico academico = await _academicoRepository.ObterAcademicoPorId(id);
            if(academico !=null) 
            {
                return File(academico.Foto, academico.FotoMimeType);
            }
            return null;
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
        public async Task<FileResult> DownloadFoto(long id)
        {
            Academico academico = await _academicoRepository.ObterAcademicoPorId(id);
            string nomeArquivo = "Foto" + academico.AcademicoID.ToString().Trim() + ".jpg";
            FileStream fileStream = new FileStream(System.IO.Path.Combine(_env.WebRootPath, nomeArquivo), FileMode.Create, FileAccess.Write);
            fileStream.Write(academico.Foto, 0, academico.Foto.Length);
            fileStream.Close();

            IFileProvider provider = new PhysicalFileProvider(_env.WebRootPath);
            IFileInfo fileInfo = provider.GetFileInfo(nomeArquivo);
            var readStream = fileInfo.CreateReadStream();
            return File(readStream, academico.FotoMimeType, nomeArquivo);
        }
    }
}