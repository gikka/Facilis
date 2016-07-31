using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class CidadeRepository : RepositoryBase<Cidade>, ICidadeRepository
    {
        public IEnumerable<Cidade> ListarPorEstado(int estadoId)
        {
            return Db.Cidades.Where(p => p.EstadoId == estadoId);
        }
    }
}
