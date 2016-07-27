﻿using AutoMapper;
using Facilis.Application.Interface;
using Facilis.MVC.ViewModels;
using Facilis.Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    public class EventosController : Controller
    {
        // GET: Evento
        private readonly IEventoAppService _eventoApp;

        public EventosController(IEventoAppService eventoApp)
        {
            _eventoApp = eventoApp;
        }
        public ActionResult Index()
        {
            var eventoViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(_eventoApp.GetAll());

            return View(eventoViewModel); ;
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        public ActionResult Create(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoApp.Add(eventoDomain);

                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                _eventoApp.Update(eventoDomain);

                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Evento/Delete/5
        public ActionResult Delete(int id)
        {
            var evento = _eventoApp.GetById(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var evento = _eventoApp.GetById(id);
            _eventoApp.Remove(evento);

            return RedirectToAction("Index");
        }
    }
}
