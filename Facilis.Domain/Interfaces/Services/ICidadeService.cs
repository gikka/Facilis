using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Services
{
    public interface ICidadeService : IServiceBase<Cidade>
    {
        IEnumerable<Cidade> ListarPorEstado(int estadoId);
    }
}
