using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Application.Interface
{
    public interface ICupomAppService : IAppServiceBase<Cupom>
    {
        IEnumerable<Cupom> ListarPorUsuario(string usuarioId);
    }
}
