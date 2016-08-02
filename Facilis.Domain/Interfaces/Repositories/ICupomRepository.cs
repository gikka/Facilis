using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface ICupomRepository : IRepositoryBase<Cupom>
    {
        IEnumerable<Cupom> ListarPorUsuario(string usuarioId);
    }
}
