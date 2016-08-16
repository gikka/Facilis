using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class ParticipanteRepository : RepositoryBase<Participante>, IParticipanteRepository
    {
        public IEnumerable<Participante> ListarPorUsuario(string usuarioId)
        {
            return Db.Participantes.Where(p => p.UsuarioId == usuarioId).OrderBy(p => p.Evento.DataInicial);
        }
    }
}
