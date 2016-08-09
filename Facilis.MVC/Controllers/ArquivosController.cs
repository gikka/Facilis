using System.Web.Mvc;
using Facilis.Application.Interface;
using AutoMapper;
using System.Collections.Generic;
using Facilis.MVC.ViewModels;
using Facilis.Domain.Entities;
using System.Web;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class ArquivosController : Controller
    {
        private readonly IArquivoAppService _arquivoApp;

        public ArquivosController(IArquivoAppService arquivoApp)
        {
            _arquivoApp = arquivoApp;
        }

        // GET: Arquivos
        public ActionResult Index()
        {
            //var listaArquivos = _arquivoApp.ListarPorEvento(eventoId);
            var listaArquivos = _arquivoApp.GetAll();
            var arquivoViewModel = Mapper.Map<IEnumerable<Arquivo>, IEnumerable<ArquivoViewModel>>(listaArquivos);

            return View(arquivoViewModel); 

        }

        // GET: Arquivo/Details/5
        public ActionResult Details(int id)
        {

            var arquivo = _arquivoApp.GetById(id);
            var arquivoViewModel = Mapper.Map<Arquivo, ArquivoViewModel>(arquivo);

            return View(arquivoViewModel);
        }

        // GET: Arquivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Arquivo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArquivoViewModel arquivo, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var arquivoDomain = Mapper.Map<ArquivoViewModel, Arquivo>(arquivo);

                if (upload != null && upload.ContentLength > 0)
                {
                    arquivoDomain.ContentType = upload.ContentType;
                  
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        arquivoDomain.Content = reader.ReadBytes(upload.ContentLength);
                    }
                    
                }

                _arquivoApp.Add(arquivoDomain);

                return RedirectToAction("Edit", "Eventos", new { id = arquivo.EventoId});
            }
            return View(arquivo);
        }

        // GET: Arquivo/Edit/5
        public ActionResult Edit(int id)
        {
            var arquivo = _arquivoApp.GetById(id);
            var arquivoViewModel = Mapper.Map<Arquivo, ArquivoViewModel>(arquivo);

            return View(arquivoViewModel);
        }

        // POST: Arquivo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ArquivoViewModel arquivo)
        {

            if (ModelState.IsValid)
            {
                var arquivoDomain = Mapper.Map<ArquivoViewModel, Arquivo>(arquivo);
                _arquivoApp.Update(arquivoDomain);

                return RedirectToAction("Edit", "Eventos", new { id = arquivo.EventoId });
            }
            
           return View(arquivo);
        }

        // GET: Arquivo/Delete/5
        public ActionResult Delete(int id)
        {

            var arquivo = _arquivoApp.GetById(id);
            var arquivoViewModel = Mapper.Map<Arquivo, ArquivoViewModel>(arquivo);

            return View(arquivoViewModel);
        }

        // POST: Arquivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var arquivo = _arquivoApp.GetById(id);
            _arquivoApp.Remove(arquivo);

            return RedirectToAction("Edit", "Eventos", new { id = arquivo.EventoId });
        }
    }
}