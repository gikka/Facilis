using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Services
{
    public interface ICupomService : IServiceBase<Cupom>
    {
        IEnumerable<Cupom> ListarPorUsuario(string usuarioId);
    }
}
