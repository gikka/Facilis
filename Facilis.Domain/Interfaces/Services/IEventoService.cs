using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Services
{
    public interface IEventoService : IServiceBase<Evento>
    {
        IEnumerable<Evento> ListarPorUsuario(string usuarioId);
        IEnumerable<Evento> ListarProximos();
    }
}
