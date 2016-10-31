using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class EventoRepository : RepositoryBase<Evento>, IEventoRepository
    {
        public IEnumerable<Evento> ListarPorUsuario(string usuarioId)
        {
            return Db.Eventos.Where(e => e.UsuarioId == usuarioId);
        }

        public IEnumerable<Evento> ListarProximos()
        {
            return Db.Eventos.Where(e => e.DataFinal.CompareTo(DateTime.Now) >= 0 && e.DataCancelamento == null);
        }

        public IEnumerable<Evento> ListarPagosPorUsuario(string usuarioId)
        {
            return Db.Eventos.Where(e => e.UsuarioId == usuarioId && e.DataCancelamento == null && e.TipoEvento == 2).OrderBy(e => e.Nome);
        }
    }
}
