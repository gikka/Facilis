using System;
using System.Collections.Generic;
using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;

namespace Facilis.Application
{
    public class EventoAppService : AppServiceBase<Evento>, IEventoAppService
    {
        private readonly IEventoService _eventoService;

        public EventoAppService(IEventoService eventoService)
            : base(eventoService)
        {
            _eventoService = eventoService;
        }

        public IEnumerable<Evento> ListarPorUsuario(string usuarioId)
        {
            return _eventoService.ListarPorUsuario(usuarioId);
        }

        public IEnumerable<Evento> ListarProximos()
        {
            return _eventoService.ListarProximos();
        }
    }
}
