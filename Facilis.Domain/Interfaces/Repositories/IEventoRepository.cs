using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface IEventoRepository : IRepositoryBase<Evento>
    {
        IEnumerable<Evento> ListarPorUsuario(string usuarioId);
    }
}
