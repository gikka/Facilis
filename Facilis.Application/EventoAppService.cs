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
    }
}
