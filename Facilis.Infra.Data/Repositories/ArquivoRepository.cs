using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class ArquivoRepository : RepositoryBase<Arquivo>, IArquivoRepository
    {
        public IEnumerable<Arquivo> ListarPorEvento(int eventoId)
        {
            return Db.Arquivos.Where(a => a.EventoId == eventoId);
        }
    }
}
