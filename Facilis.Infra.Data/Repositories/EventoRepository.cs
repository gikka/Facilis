using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class EventoRepository : RepositoryBase<Evento>, IEventoRepository
    {
        public IEnumerable<Evento> ListarPorUsuario(string usuarioId)
        {
            return Db.Eventos.Where(e => e.UsuarioId == usuarioId);
        }
    }
}
