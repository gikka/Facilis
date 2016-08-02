using System;
using System.Collections.Generic;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class CupomRepository : RepositoryBase<Cupom>, ICupomRepository
    {
        public IEnumerable<Cupom> ListarPorUsuario(string usuarioId)
        {
            return Db.Cupons.Where(c => c.UsuarioId == usuarioId);
        }
    }
}
