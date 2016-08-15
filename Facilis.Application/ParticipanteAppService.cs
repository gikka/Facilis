using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;

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
    }
}
