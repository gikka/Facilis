using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository : IRepositoryBase<Cidade>
    {
        IEnumerable<Cidade> ListarPorEstado(int estadoId);
    }
}
