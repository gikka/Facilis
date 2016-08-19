using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class ParticipanteRepository : RepositoryBase<Participante>, IParticipanteRepository
    {
        public IEnumerable<Participante> ListarPorUsuario(string usuarioId)
        {
            return Db.Participantes.Where(p => p.UsuarioId == usuarioId).OrderBy(p => p.Evento.DataInicial);
        }

        public IEnumerable<Participante> ListarInscritosPorEvento(int eventoId)
        {
            return Db.Participantes.Where(p => p.EventoId == eventoId).OrderBy(p => p.Usuario.Nome);
        }

        public void MarcarPresenca(int id)
        {
            Db.Participantes.Find(id).Presenca = true;
            Db.SaveChanges();
        }
    }
}
