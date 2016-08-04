using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface IArquivoRepository : IRepositoryBase<Arquivo>
    {
        IEnumerable<Arquivo> ListarPorEvento(int eventoId);
    }
}
