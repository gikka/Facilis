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
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;

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

            participanteViewModel.Usuario = usuarioViewModel;
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
                participante.Usuario = usuarioViewModel;

                //não permitir que o usuário se inscreva nos próprios eventos
                if (participanteDomain.UsuarioId == evento.UsuarioId)
                {
                    ModelState.AddModelError("", "Você não pode se inscrever no próprio evento.");
                    return View(participante);
                }

                //TODO: antes de efetivar a inscrição verificar se o usuario já não está inscrito no evento
                //TODO: se for utilizar cupom de desconto validar se  o nome do cupom existe, buscar o id e gravar

                participanteDomain.DataInscricao = DateTime.Now;
                participanteDomain.DataCancelamento = null;
                _participanteApp.Add(participanteDomain);

                //enviar e-mail confirmando que a inscrição for realizada
                var assunto = "Inscrição confirmada";
                var mensagem = string.Format("Sua inscrição no evento {0} em {1:dd/MM/yyyy} {2} foi realizada com sucesso!", evento.Nome, evento.DataInicial, evento.HoraInicial);
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

            //TODO: O sistema deve permitir que os participantes efetuem o cancelamento da inscrição até a data do evento
            //TODO: comparar com a data do evento
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
                p.Usuario = usuarioViewModel;
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
                participante.Usuario = usuarioViewModel;

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

        public ActionResult Cracha(int id)
        {
            var participante = _participanteApp.GetById(id);
            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);

            _participanteApp.RegistrarEmissaoCracha(id);

            return new PdfActionResult("Cracha", participanteViewModel, (writer, document) =>
            {
                document.SetPageSize(new Rectangle(300f, 400f, 90));
                document.NewPage();
            });
        }

        public ActionResult Certificado(int id)
        {
            var participante = _participanteApp.GetById(id);
            var participanteViewModel = Mapper.Map<Participante, ParticipanteViewModel>(participante);
            var html = new StringBuilder();

            _participanteApp.RegistrarEmissaoCertificado(id);

            html.Append("div class='container'>");
            html.Append("<div class='borda-certificado'>");
            html.Append("<h1 class='titulo-certificado'>Certificado de Participação</h1>");
            html.Append("<hr />");
            html.AppendFormat("<h2 class='usuario'>{0} {1}</h2>", participante.Usuario.Nome, participante.Usuario.Sobrenome);
            html.Append("<h3 class='evento'>");
            html.Append("");
            html.Append("");
            html.Append("");
            html.Append("</h3>");
            html.Append("</div>");
            html.Append("</div>");

            var css = ".titulo-certificado {margin: 40px 0 0;border-bottom: 1px solid #eeeeee;text-align: center;font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-weight: 500; font-size: 70px;  }";


            return new PdfActionResult(participante, (writer, document) =>
            {
                document.Open();
                document.SetPageSize(PageSize.A4);
                document.NewPage();
                //Image imagemFundo = Image.GetInstance(Server.MapPath(@"..\..\fonts\Icones\bonito.png"));
                //imagemFundo.ScaleAbsolute(PageSize.A4);
               // imagemFundo.SetAbsolutePosition(0, 0);
                //document.Add(imagemFundo);

                using (var msCss = new MemoryStream(Encoding.UTF8.GetBytes(css)))
                {
                    using (var msHtml = new MemoryStream(Encoding.UTF8.GetBytes(html.ToString())))
                    {
                        iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, msHtml, msCss);
                    }
                }
                document.Close();
            });


            //document.SetPageSize(new Rectangle(1000f, 500f, 90));

        }

        public ActionResult ImprimirListaPresenca(int id)
        {
            var lista = _participanteApp.ListarInscritosPorEvento(id).Where(f => f.Presenca == true).ToList();

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            if (lista.Count == 0)
            {
                ViewBag.Mensagem = "Nenhum inscrito com presença confirmada até o momento.";
            }
            else
            {
                ViewBag.Evento = participanteViewModel.Select(f => f.Evento.Nome).First();
            }

            return new PdfActionResult("ListaParticipantes", participanteViewModel);
        }

        public ActionResult RelatorioPorRegiao()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        public ActionResult RelatorioPorRegiaoImpressao(int id)
        {
            var lista = _participanteApp.ListarInscritosPorEvento(id).ToList();

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            var listaAgrupada = (from p in participanteViewModel
                                 group p by new { p.Usuario.Estado.Sigla, p.Usuario.Cidade.Nome }
                                 into grp
                                 select new ParticipanteViewModel
                                 {
                                     Usuario = new RegisterViewModel
                                     {
                                         Estado = new Estado { Sigla = grp.Key.Sigla },
                                         Cidade = new Cidade { Nome = grp.Key.Nome }
                                     },
                                     Quantidade = grp.Count()
                                 }).OrderBy(f => f.Usuario.Estado.Sigla).ThenBy(f => f.Usuario.Cidade.Nome);

            if (lista.Count > 0)
            {
                ViewBag.Evento = participanteViewModel.Select(f => f.Evento.Nome).First();
                ViewBag.Total = listaAgrupada.Sum(f => f.Quantidade);
            }

            return new PdfActionResult("RelatorioPorRegiaoImpressao", listaAgrupada);
        }

        public ActionResult RelatorioPorSexo()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        public ActionResult RelatorioPorSexoImpressao(int id)
        {
            var lista = _participanteApp.ListarInscritosPorEvento(id).ToList();

            var participanteViewModel = Mapper.Map<IEnumerable<Participante>, IEnumerable<ParticipanteViewModel>>(lista);

            var listaAgrupada = (from p in participanteViewModel
                                 group p by new { p.Usuario.Sexo }
                                 into grp
                                 select new ParticipanteViewModel
                                 {
                                     Usuario = new RegisterViewModel { Sexo = grp.Key.Sexo == "F" ? "Feminino" : "Masculino" },
                                     Quantidade = grp.Count()
                                 });

            if (lista.Count > 0)
            {
                ViewBag.Evento = participanteViewModel.Select(f => f.Evento.Nome).First();
                ViewBag.Total = listaAgrupada.Sum(f => f.Quantidade);
            }

            return new PdfActionResult("RelatorioPorSexoImpressao", listaAgrupada);
        }


        // GET: 
        public ActionResult GraficoPorRegiao()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        //POST
        [AllowAnonymous]
        public ActionResult ContarPorRegiao(int eventoId) => Json((from p in _participanteApp.ContarPorRegiao(eventoId).ToList()
                                                                   group p by new { p.Usuario.Estado.Sigla, p.Usuario.Cidade.Nome }
                                                                   into grp
                                                                   select new
                                                                   {
                                                                       grp.Key.Sigla,
                                                                       grp.Key.Nome,
                                                                       Quantidade = grp.Count()
                                                                   }).ToList(), JsonRequestBehavior.AllowGet);

        // GET: 
        public ActionResult GraficoPorSexo()
        {
            CarregarDropDownEvento(null);

            return View();
        }

        //POST
        [AllowAnonymous]
        public ActionResult ContarPorSexo(int eventoId) => Json((from p in _participanteApp.ContarPorSexo(eventoId).ToList()
                                                                 group p by new { p.Usuario.Sexo }
                                                                 into grp
                                                                 select new
                                                                 {
                                                                     Sexo = grp.Key.Sexo == "F" ? "Feminino" : "Masculino",
                                                                     Quantidade = grp.Count()
                                                                 }).ToList(), JsonRequestBehavior.AllowGet);


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
