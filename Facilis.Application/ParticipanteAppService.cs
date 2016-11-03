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

        public IEnumerable<Participante> ListarInscritosAtivosPorEvento(int eventoId)
        {
            return _participanteService.ListarInscritosAtivosPorEvento(eventoId);
        }

        public void MarcarPresenca(int id)
        {
            _participanteService.MarcarPresenca(id);
        }

        public IEnumerable<Participante> ContarPorRegiao(int eventoId)
        {
            return _participanteService.ContarPorRegiao(eventoId);
        }

        public IEnumerable<Participante> ContarPorSexo(int eventoId)
        {
            return _participanteService.ContarPorSexo(eventoId);
        }

        public int ContarVagasUtilizadas(int eventoId)
        {
            return _participanteService.ContarVagasUtilizadas(eventoId);
        }

        public int ContarInscritos(int eventoId)
        {
            return _participanteService.ContarInscritos(eventoId);
        }

        public void RegistrarEmissaoCracha(int id)
        {
            _participanteService.RegistrarEmissaoCracha(id);
        }

        public void RegistrarEmissaoCertificado(int id)
        {
            _participanteService.RegistrarEmissaoCertificado(id);
        }

    }
}
