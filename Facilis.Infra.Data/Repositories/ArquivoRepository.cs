using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class ArquivoRepository : RepositoryBase<Arquivo>, IArquivoRepository
    {
        public IEnumerable<Arquivo> ListarPorEvento(int eventoId)
        {
            return Db.Arquivos.Where(a => a.EventoId == eventoId);
        }

        public new void Update(Arquivo obj)
        {
            var local = Db.Set<Arquivo>()
                                     .Local
                                     .FirstOrDefault(f => f.ArquivoId == obj.ArquivoId);

            Db.Entry(local).State = EntityState.Detached;
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }
    }
}
