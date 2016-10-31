using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.MVC.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class CuponsController : Controller
    {
        private readonly ICupomAppService _cupomApp;
        private readonly IEventoAppService _eventoApp;

        public CuponsController(ICupomAppService cupomApp, IEventoAppService eventoApp)
        {
            _cupomApp = cupomApp;
            _eventoApp = eventoApp;
        }


        // GET: Cupom
        public ActionResult Index()
        {
            var listaCupons = _cupomApp.ListarPorUsuario(User.Identity.GetUserId());
            var cupomViewModel = Mapper.Map<IEnumerable<Cupom>, IEnumerable<CupomViewModel>>(listaCupons);

            return View(cupomViewModel); ;
        }

        // GET: Cupom/Details/5
        public ActionResult Details(int id)
        {

            var cupom = _cupomApp.GetById(id);
            var cupomViewModel = Mapper.Map<Cupom, CupomViewModel>(cupom);

            CarregarDropDownEvento(cupom.EventoId);
            return View(cupomViewModel);
        }

        // GET: Cupom/Create
        public ActionResult Create()
        {
            CarregarDropDownEvento(null);
            return View();
        }

        // POST: Cupom/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CupomViewModel cupom)
        {
            if (ModelState.IsValid)
            {
                var cupomDomain = Mapper.Map<CupomViewModel, Cupom>(cupom);
                cupomDomain.UsuarioId = User.Identity.GetUserId();
                _cupomApp.Add(cupomDomain);

                return RedirectToAction("Index");
            }

            CarregarDropDownEvento(cupom.EventoId);

            return View(cupom);
        }

        // GET: Cupom/Edit/5
        public ActionResult Edit(int id)
        {
            var cupom = _cupomApp.GetById(id);
            var cupomViewModel = Mapper.Map<Cupom, CupomViewModel>(cupom);

            CarregarDropDownEvento(cupom.EventoId);
            return View(cupomViewModel);
        }

        // POST: Cupom/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CupomViewModel cupom)
        {

            if (ModelState.IsValid)
            {
                var cupomDomain = Mapper.Map<CupomViewModel, Cupom>(cupom);
                cupomDomain.UsuarioId = User.Identity.GetUserId();
                _cupomApp.Update(cupomDomain);

                return RedirectToAction("Index");
            }

            CarregarDropDownEvento(cupom.EventoId);
            return View(cupom);
        }

        // GET: Cupom/Delete/5
        public ActionResult Delete(int id)
        {

            var cupom = _cupomApp.GetById(id);
            var cupomViewModel = Mapper.Map<Cupom, CupomViewModel>(cupom);

            return View(cupomViewModel);
        }

        // POST: Cupom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cupom = _cupomApp.GetById(id);
            _cupomApp.Remove(cupom);

            return RedirectToAction("Index");
        }

        private void CarregarDropDownEvento(int ?id)
        {
            if (id == null)
            {
                ViewBag.EventoList = new SelectList(_eventoApp.ListarPagosPorUsuario(User.Identity.GetUserId()), "EventoId", "Nome");
            }
            else
            {
                ViewBag.EventoList = new SelectList(_eventoApp.ListarPagosPorUsuario(User.Identity.GetUserId()), "EventoId", "Nome", id);
            }
            
        }
    }
}
