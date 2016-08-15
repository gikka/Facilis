using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;

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

    }
}
