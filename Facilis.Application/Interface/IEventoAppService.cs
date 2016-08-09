using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Application.Interface
{
    public interface IEventoAppService : IAppServiceBase<Evento>
    {
        IEnumerable<Evento> ListarPorUsuario(string usuarioId);
        IEnumerable<Evento> ListarProximos();
    }
}
