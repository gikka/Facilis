using AutoMapper;
using Facilis.Application.Interface;
using Facilis.MVC.ViewModels;
using Facilis.Domain.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;

namespace Facilis.MVC.Controllers
{
    [Authorize]
    public class EventosController : Controller
    {
        // GET: Evento
        private readonly IEventoAppService _eventoApp;
        private readonly IArquivoAppService _arquivoApp;
        private readonly IVideoAppService _videoApp;
        private readonly IParticipanteAppService _participanteApp;

        public EventosController(IEventoAppService eventoApp, IArquivoAppService arquivoApp, IVideoAppService videoApp, IParticipanteAppService participanteApp)
        {
            _eventoApp = eventoApp;
            _arquivoApp = arquivoApp;
            _videoApp = videoApp;
            _participanteApp = participanteApp;
        }


        public ActionResult Index()
        {
            var listaEventos = _eventoApp.ListarPorUsuario(User.Identity.GetUserId());
            var eventoViewModel = Mapper.Map<IEnumerable<Evento>, IEnumerable<EventoViewModel>>(listaEventos);

            eventoViewModel.Each(e => e.VagasUtilizadas = _participanteApp.ContarVagasUtilizadas(e.EventoId));
            return View(eventoViewModel);
        }

        // GET: Evento/Details/5
        public ActionResult Details(int id)
        {
            var evento = _eventoApp.GetById(id);

            evento.Arquivos = _arquivoApp.ListarPorEvento(id);
            evento.Videos = _videoApp.ListarPorEvento(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            eventoViewModel.VagasUtilizadas = _participanteApp.ContarVagasUtilizadas(id);

            return View(eventoViewModel);
        }

        // GET: Evento/Create
        public ActionResult Create()
        {
            CarregarDropDownTipoEvento();
            return View();
        }

        // POST: Evento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoViewModel evento)
        {
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                eventoDomain.UsuarioId = User.Identity.GetUserId();
                _eventoApp.Add(eventoDomain);

                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Evento/Edit/5
        public ActionResult Edit(int id)
        {
            CarregarDropDownTipoEvento();

            var evento = _eventoApp.GetById(id);
            evento.Arquivos = _arquivoApp.ListarPorEvento(id);
            evento.Videos = _videoApp.ListarPorEvento(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            return View(eventoViewModel);
        }

        // POST: Evento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventoViewModel evento)
        {
            //TODO: o evento não pode estar cancelado para ser alterado. Não mostrar o botão de alterar para eventos cancelados. Fazer isso também na tela de detalhes que possui link para a alteração
            if (ModelState.IsValid)
            {
                var eventoDomain = Mapper.Map<EventoViewModel, Evento>(evento);
                eventoDomain.UsuarioId = User.Identity.GetUserId();
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

            var inscritos = _participanteApp.ContarInscritos(id);

            ViewBag.MensagemExclusao = inscritos > 0 ? "Evento possui inscrição(ões) efetuada(s) não pode ser excluído. Confirma o cancelamento do evento?" : "Confirma exclusão do evento?";

            return View(eventoViewModel);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var evento = _eventoApp.GetById(id);
            var inscritos = _participanteApp.ContarInscritos(id);

            if (inscritos > 0)
            {
                _eventoApp.Update(evento);

                //enviar e-mail aos inscritos informando o cancelamento do evento
                var assunto = "Evento Cancelado";
                var mensagem = string.Format("O evento {0} no qual você tinha realizado inscrição foi cancelado em {1}.", evento.Nome, DateTime.Now.ToString("{0:dd/MM/yyyy}"));

                var todosInscritos = _participanteApp.ListarInscritosAtivosPorEvento(id);

                var destinatarios = new List<string>();

                destinatarios = (from i in todosInscritos
                                 select i.Usuario.Email).ToList();                

                EmailService.SendAsync(assunto, mensagem, destinatarios);
            }
            else
            {
                _eventoApp.Remove(evento);
            }

            return RedirectToAction("Index");
        }

        // GET: Evento/DetalhesInscricao
        [AllowAnonymous]
        public ActionResult DetalhesInscricao(int id)
        {
            var evento = _eventoApp.GetById(id);
            evento.Arquivos = _arquivoApp.ListarPorEvento(id);
            evento.Videos = _videoApp.ListarPorEvento(id);
            var eventoViewModel = Mapper.Map<Evento, EventoViewModel>(evento);

            ViewBag.VagasUtilizadas = _participanteApp.ContarVagasUtilizadas(id);

            return View(eventoViewModel);
        }

        private void CarregarDropDownTipoEvento()
        {
            var TipoEventoList = new List<dynamic>();
            TipoEventoList.Add(new { Id = "", Text = "" });
            TipoEventoList.Add(new { Id = "1", Text = "Gratuito" });
            TipoEventoList.Add(new { Id = "2", Text = "Pago" });
            ViewBag.TipoEventoList = new SelectList(TipoEventoList, "Id", "Text");

        }

    }
}
