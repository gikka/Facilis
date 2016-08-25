using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEventoAppService _eventoApp;
        private readonly IParticipanteAppService _participanteApp;

        public EmailController(IEventoAppService eventoService, IParticipanteAppService participante)
        {
            _eventoApp = eventoService;
            _participanteApp = participante;
        }

        // GET: TinyMCE
        public ActionResult Index(int id)
        {
            var evento = _eventoApp.GetById(id);

            var email = new Email
            {
                Evento = evento,
                EventoId = evento.EventoId
            };
            
            var emailViewModel = Mapper.Map<Email, EmailViewModel>(email);
            emailViewModel.EventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(emailViewModel);
        }

        //Post
        [HttpPost]
        public ActionResult Index(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var assunto = model.Assunto.Trim();
                var mensagem = model.HtmlContent.Trim();

                var inscritos = _participanteApp.ListarInscritosPorEvento(model.EventoId).Where(p => p.DataCancelamento == null).ToList();

                var destinatarios = new List<string>();
                destinatarios = (from i in inscritos
                                 select i.Usuario.Email).ToList();

                EmailService.SendAsync(assunto, mensagem, destinatarios);
                ModelState.AddModelError("", "Email enviado com sucesso.");
            }

            var evento = _eventoApp.GetById(model.EventoId);
            model.EventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);
            
            return View(model);
        }
    }
}