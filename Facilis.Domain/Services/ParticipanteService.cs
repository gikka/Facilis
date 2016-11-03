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

        public IEnumerable<Participante> ListarInscritosAtivosPorEvento(int eventoId)
        {
            return _participanteRepository.ListarInscritosAtivosPorEvento(eventoId);
        }

        public void MarcarPresenca(int id)
        {
            _participanteRepository.MarcarPresenca(id);
        }

        public IEnumerable<Participante> ContarPorRegiao(int eventoId)
        {
            return _participanteRepository.ContarPorRegiao(eventoId);
        }

        public IEnumerable<Participante> ContarPorSexo(int eventoId)
        {
            return _participanteRepository.ContarPorSexo(eventoId);
        }

        public int ContarVagasUtilizadas(int eventoId)
        {
            return _participanteRepository.ContarVagasUtilizadas(eventoId);
        }

        public int ContarInscritos(int eventoId)
        {
            return _participanteRepository.ContarInscritos(eventoId);
        }

        public void RegistrarEmissaoCracha(int id)
        {
            _participanteRepository.RegistrarEmissaoCracha(id);
        }

        public void RegistrarEmissaoCertificado(int id)
        {
            _participanteRepository.RegistrarEmissaoCertificado(id);
        }

    }
}
