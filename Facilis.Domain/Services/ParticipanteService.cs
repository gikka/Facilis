using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class ParticipanteService : ServiceBase<Participante>, IParticipanteService
    {
        private readonly IParticipanteRepository _participanteRepository;

        public ParticipanteService(IParticipanteRepository participanteRepository)
            : base(participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public IEnumerable<Participante> ListarPorUsuario(string usuarioId)
        {
            return _participanteRepository.ListarPorUsuario(usuarioId);
        }

        public IEnumerable<Participante> ListarInscritosPorEvento(int eventoId)
        {
            return _participanteRepository.ListarInscritosPorEvento(eventoId);
        }

        public void MarcarPresenca(int id)
        {
            _participanteRepository.MarcarPresenca(id);
        }

    }
}
