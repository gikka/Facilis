using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface IParticipanteRepository : IRepositoryBase<Participante>
    {
        IEnumerable<Participante> ListarPorUsuario(string usuarioId);
        IEnumerable<Participante> ListarInscritosPorEvento(int eventoId);
        IEnumerable<Participante> ListarInscritosAtivosPorEvento(int eventoId);
        void MarcarPresenca(int id);
        IEnumerable<Participante> ContarPorRegiao(int eventoId);
        IEnumerable<Participante> ContarPorSexo(int evendoId);
        int ContarVagasUtilizadas(int eventoId);
        int ContarInscritos(int eventoId);
    }
}
