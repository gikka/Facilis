using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class EventoService : ServiceBase<Evento>, IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
            : base(eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public IEnumerable<Evento> ListarPorUsuario(string usuarioId)
        {
            return _eventoRepository.ListarPorUsuario(usuarioId);
        }

        public IEnumerable<Evento> ListarProximos()
        {
            return _eventoRepository.ListarProximos();
        }
    }
}
