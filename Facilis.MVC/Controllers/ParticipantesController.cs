using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Configuration;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.MVC.ViewModels;
using iTextSharp.text;
using Microsoft.AspNet.Identity;
using MvcRazorToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class ParticipantesController : Controller
    {
        private readonly IParticipanteAppService _participanteApp;
        private readonly IEventoAppService _eventoApp;
        private ApplicationUserManager _userManager;


        public ParticipantesController(IParticipanteAppService participanteService, IEventoAppService eventoService, ApplicationUserManager userManager)
        {
            _participanteApp = participanteService;
            _eventoApp = eventoService;
            _userManager = userManager;
        }

        // GET: Participante
        public ActionResult Index()
        {
            var lista = _participanteApp.ListarPorUsuario(User.Identity.GetUserId());
            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            return View(participanteViewModel);
        }

        // GET: Evento/DetalhesInscricao
        public ActionResult Inscricao(int id)
        {
            var evento = _eventoApp.GetById(id);
            var usuario = _userManager.FindById(User.Identity.GetUserId());

            var participante = new Participante
            {

                Usuario = usuario.usuario,
                UsuarioId = usuario.Id,
                Evento = evento,
                EventoId = id

            };

            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);
            var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            participanteViewModel.UsuarioViewModel = usuarioViewModel;
            participanteViewModel.Evento = eventoViewModel;

            CarregarDropDownFormaPagamento();

            return View(participanteViewModel);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Inscricao(ParticipanteViewModel participante)
        {
            CarregarDropDownFormaPagamento();

            if (ModelState.IsValid)
            {
                var participanteDomain = Mapper.Map<ParticipanteViewModel, Participante>(participante);
                participanteDomain.UsuarioId = User.Identity.GetUserId();

                var evento = _eventoApp.GetById(participanteDomain.EventoId);
                var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

                var usuario = _userManager.FindById(User.Identity.GetUserId());
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);

                participante.Evento = eventoViewModel;
                participante.UsuarioViewModel = usuarioViewModel;

                //não permitir que o usuário se inscreva nos próprios eventos
                if (participanteDomain.UsuarioId == evento.UsuarioId)
                {
                    ModelState.AddModelError("", "Você não pode se inscrever no próprio evento.");
                    return View(participante);
                }

                participanteDomain.DataInscricao = DateTime.Now;
                participanteDomain.DataCancelamento = null;
                _participanteApp.Add(participanteDomain);

                //enviar e-mail confirmando que a inscrição for realizada
                var assunto = "Inscrição confirmada";
                var mensagem = string.Format("Sua inscrição no evento {0} em {1} {2} foi realizada com sucesso!", evento.Nome, evento.DataInicial, evento.HoraInicial);
                var destinatario = new List<string>();
                destinatario.Add(usuario.Email);

                EmailService.SendAsync(assunto, mensagem, destinatario);

                return RedirectToAction("Index");
            }

            return View(participante);
        }

        // GET: Cancelamento
        public ActionResult CancelarInscricao(int id)
        {
            var participante = _participanteApp.GetById(id);
            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);

            return View(participanteViewModel);
        }

        // POST: Cancelamento
        [HttpPost, ActionName("CancelarInscricao")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarCancelarInscricao(int id)
        {
            var participante = _participanteApp.GetById(id);

            participante.DataCancelamento = DateTime.Now;
            _participanteApp.Update(participante);

            return RedirectToAction("Index");
        }

        // GET: 
        public ActionResult ControleParticipantes(int id)
        {
            var lista = _participanteApp.ListarInscritosPorEvento(id);

            var evento = _eventoApp.GetById(id);

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            ViewBag.Evento = evento.Nome;
            ViewBag.EventoId = evento.EventoId;

            foreach (ParticipanteViewModel p in participanteViewModel)
            {
                var usuario = _userManager.FindById(p.UsuarioId);
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
                p.UsuarioViewModel = usuarioViewModel;
            }

            return View(participanteViewModel);
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ControleParticipantes(ParticipanteViewModel participante)
        {
            CarregarDropDownFormaPagamento();

            if (ModelState.IsValid)
            {
                var participanteDomain = Mapper.Map<ParticipanteViewModel, Participante>(participante);
                participanteDomain.UsuarioId = User.Identity.GetUserId();

                var evento = _eventoApp.GetById(participanteDomain.EventoId);
                var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

                var usuario = _userManager.FindById(User.Identity.GetUserId());
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);

                participante.Evento = eventoViewModel;
                participante.UsuarioViewModel = usuarioViewModel;

                //não permitir que o usuário se inscreva nos próprios eventos
                if (participanteDomain.UsuarioId == evento.UsuarioId)
                {
                    ModelState.AddModelError("", "Você não pode se inscrever no próprio evento.");
                    return View(participante);
                }

                participanteDomain.DataInscricao = DateTime.Now;
                participanteDomain.DataCancelamento = null;
                _participanteApp.Add(participanteDomain);

                return RedirectToAction("Index");
            }

            return View(participante);
        }


        public ActionResult MarcarPresenca(int id, int eventoId)
        {

            _participanteApp.MarcarPresenca(id);
            return RedirectToAction("ControleParticipantes", new { id = eventoId });
        }

        public ActionResult Cracha(int id, int eventoId)
        {
            var participante = _participanteApp.GetById(id);
            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);

            var evento = _eventoApp.GetById(eventoId);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            var usuario = _userManager.FindById(participante.UsuarioId);
            var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);

            participanteViewModel.Evento = eventoViewModel;
            participanteViewModel.UsuarioViewModel = usuarioViewModel;

            return new PdfActionResult("Cracha", participanteViewModel);
        }

        public ActionResult Certificado(int id, int eventoId)
        {
            var participante = _participanteApp.GetById(id);
            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);

            var evento = _eventoApp.GetById(eventoId);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            var usuario = _userManager.FindById(participante.UsuarioId);
            var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);

            participanteViewModel.Evento = eventoViewModel;
            participanteViewModel.UsuarioViewModel = usuarioViewModel;

            return new PdfActionResult("Certificado", participanteViewModel, (writer, document) =>
            {
                document.SetPageSize(new Rectangle(1000f, 500f, 90));
                document.NewPage(); 
            });
        }

        public ActionResult ImprimirListaPresenca()
        {
            //var lista = _participanteApp.ListarInscritosPorEvento(id);
            var lista = _participanteApp.GetAll();
            //var evento = _eventoApp.GetById(id);

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            //ViewBag.Evento = evento.Nome;

            foreach (ParticipanteViewModel p in participanteViewModel)
            {
                var usuario = _userManager.FindById(p.UsuarioId);
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
                p.UsuarioViewModel = usuarioViewModel;
               
            }

            return new PdfActionResult("ListaParticipantes", participanteViewModel);
        }

        public ActionResult RelatorioPorRegiao()
        {
            //var lista = _participanteApp.ListarInscritosPorEvento(id);
            var lista = _participanteApp.GetAll();
            //var evento = _eventoApp.GetById(id);

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            //ViewBag.Evento = evento.Nome;

            foreach (ParticipanteViewModel p in participanteViewModel)
            {
                var usuario = _userManager.FindById(p.UsuarioId);
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
                p.UsuarioViewModel = usuarioViewModel;

            }

            return new PdfActionResult("RelatorioPorRegiao", participanteViewModel);
        }

        public ActionResult RelatorioPorSexo()
        {
            //var lista = _participanteApp.ListarInscritosPorEvento(id);
            var lista = _participanteApp.GetAll();
            //var evento = _eventoApp.GetById(id);

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            //ViewBag.Evento = evento.Nome;

            foreach (ParticipanteViewModel p in participanteViewModel)
            {
                var usuario = _userManager.FindById(p.UsuarioId);
                var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
                p.UsuarioViewModel = usuarioViewModel;

            }
            return new PdfActionResult("RelatorioPorSexo", participanteViewModel);
        }


        // GET: 
        public ActionResult GraficoPorRegiao()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        //POST
        [AllowAnonymous]
        public ActionResult ContarPorRegiao(int eventoId) => Json(_participanteApp.ContarPorRegiao(eventoId).ToList(), JsonRequestBehavior.AllowGet);


        // GET: 
        public ActionResult GraficoPorSexo()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        //POST
        [AllowAnonymous]
        public ActionResult ContarPorSexo(int eventoId) => Json(_participanteApp.ContarPorSexo(eventoId).ToList(), JsonRequestBehavior.AllowGet);


        private void CarregarDropDownFormaPagamento()
        {
            var FormaPagamentoList = new List<dynamic>();
            FormaPagamentoList.Add(new { Id = "", Text = "" });
            FormaPagamentoList.Add(new { Id = "1", Text = "Boleto" });
            FormaPagamentoList.Add(new { Id = "2", Text = "Cartão de Crédito" });
            ViewBag.FormaPagamentoList = new SelectList(FormaPagamentoList, "Id", "Text");

        }

        private void CarregarDropDownEvento(int? id)
        {
            if (id == null)
            {
                ViewBag.EventoList = new SelectList(_eventoApp.ListarPorUsuario(User.Identity.GetUserId()).OrderBy(e => e.Nome), "EventoId", "Nome");
            }
            else
            {
                ViewBag.EventoList = new SelectList(_eventoApp.ListarPorUsuario(User.Identity.GetUserId()).OrderBy(e => e.Nome), "EventoId", "Nome", id);
            }
        }

    }


}
