using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.MVC.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private readonly IVideoAppService _videoApp;
        private readonly IEventoAppService _eventoApp;

        public VideosController(IVideoAppService videoApp, IEventoAppService eventoApp)
        {
            _videoApp = videoApp;
            _eventoApp = eventoApp;
        }

        // GET: Videos
        public ActionResult Index(int id)
        {
            var lista = _videoApp.ListarPorEvento(id);
            var videoViewModel = Mapper.Map<IEnumerable<Video>, IEnumerable<VideoViewModel>>(lista);

            return View(videoViewModel);
        }

        // GET: Videos/Details/5
        public ActionResult Details(int id)
        {
            var video = _videoApp.GetById(id);
            var videoViewModel = Mapper.Map<Video, VideoViewModel>(video);

            return View(videoViewModel);
        }

        // GET: Videos/Create
        public ActionResult Create(int id)
        {
            ViewBag.Evento = _eventoApp.GetById(id).Nome;
            ViewBag.EventoId = id;
            return View();
        }

        // POST: Videos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VideoViewModel video)
        {
            if (ModelState.IsValid)
            {
                var videoDomain = Mapper.Map<VideoViewModel, Video>(video);

                _videoApp.Add(videoDomain);

                return RedirectToAction("Edit", "Eventos", new { id = video.EventoId });
            }
            return View(video);

        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int id)
        {
            var video = _videoApp.GetById(id);
            var videoViewModel = Mapper.Map<Video, VideoViewModel>(video);

            return View(videoViewModel);
        }

        // POST: Videos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VideoViewModel video)
        {
            if (ModelState.IsValid)
            {
                var videoDomain = Mapper.Map<VideoViewModel, Video>(video);
                _videoApp.Update(videoDomain);

                return RedirectToAction("Edit", "Eventos", new { id = video.EventoId });
            }

            return View(video);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int id)
        {

            var video = _videoApp.GetById(id);
            var videoViewModel = Mapper.Map<Video, VideoViewModel>(video);

            return View(videoViewModel);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var video = _videoApp.GetById(id);
            _videoApp.Remove(video);

            return RedirectToAction("Edit", "Eventos", new { id = video.EventoId });

        }
    }
}
