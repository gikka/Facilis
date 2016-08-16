using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface IParticipanteRepository : IRepositoryBase<Participante>
    {
        IEnumerable<Participante> ListarPorUsuario(string usuarioId);
    }
}
