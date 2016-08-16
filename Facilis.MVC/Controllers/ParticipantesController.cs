using AutoMapper;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Configuration;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.MVC.ViewModels;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
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
                Evento = evento
               
            };

            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);
            var usuarioViewModel = Mapper.Map<Usuario, RegisterViewModel>(usuario.usuario);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            participanteViewModel.UsuarioViewModel = usuarioViewModel;
            participanteViewModel.EventoViewModel = eventoViewModel;

            CarregarDropDownFormaPagamento();

            return View(participanteViewModel);
        }

        private void CarregarDropDownFormaPagamento()
        {
            var FormaPagamentoList = new List<dynamic>();
            FormaPagamentoList.Add(new { Id = "", Text = "" });
            FormaPagamentoList.Add(new { Id = "1", Text = "Boleto" });
            FormaPagamentoList.Add(new { Id = "2", Text = "Cartão de Crédito" });
            ViewBag.FormaPagamentoList = new SelectList(FormaPagamentoList, "Id", "Text");

        }

    }


}
