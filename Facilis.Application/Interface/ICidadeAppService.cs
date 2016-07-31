using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Application.Interface
{
    public interface ICidadeAppService : IAppServiceBase<Cidade>
    {
        IEnumerable<Cidade> ListarPorEstado(int estadoId);
    }
}
