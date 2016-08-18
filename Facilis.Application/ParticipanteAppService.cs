using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Application
{
    public class ParticipanteAppService : AppServiceBase<Participante>, IParticipanteAppService
    {
        private readonly IParticipanteService _participanteService;
        public ParticipanteAppService(IParticipanteService participanteService) 
            : base(participanteService)
        {
            _participanteService = participanteService;
        }

        public IEnumerable<Participante> ListarPorUsuario(string usuarioId)
        {
            return _participanteService.ListarPorUsuario(usuarioId);
        }

        public IEnumerable<Participante> ListarInscritosPorEvento(int eventoId)
        {
            return _participanteService.ListarInscritosPorEvento(eventoId);
        }
    }
}
