using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventoAppService _eventoApp;

        public HomeController(IEventoAppService eventoApp)
        {
            _eventoApp = eventoApp;
        }

        public ActionResult Index()
        {
            var listaEventos = _eventoApp.ListarProximos();
            var eventoViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(listaEventos);

            return View(eventoViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Facilis - sistema gerenciador de eventos";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}