using Facilis.Application.Interface;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class ParticipanteController : Controller
    {
        private readonly IParticipanteAppService _participanteApp;

        public ParticipanteController(IParticipanteAppService participanteService)
        {
            _participanteApp = participanteService;
        }

        // GET: Participante
        public ActionResult Index()
        {
            return View();
        }

        // GET: Participante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Participante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Participante/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Participante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Participante/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Participante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Participante/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
