using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Application.Interface
{
    public interface IArquivoAppService : IAppServiceBase<Arquivo>
    {
        IEnumerable<Arquivo> ListarPorEvento(int eventoId);
    }
}
