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

        public IEnumerable<Participante> ListarInscritosAtivosPorEvento(int eventoId)
        {
            return Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null).OrderBy(p => p.Usuario.Nome);
        }

        public List<string> ListarEmailsInscritos(int eventoId)
        {
            return Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null).OrderBy(p => p.Usuario.Nome).Select(p => p.Usuario.Email).ToList();
        }


        public void MarcarPresenca(int id)
        {
            Db.Participantes.Find(id).Presenca = true;
            Db.SaveChanges();
        }

        public IEnumerable<Participante> ContarPorRegiao(int eventoId)
        {
            return Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null);
        }

        public IEnumerable<Participante> ContarPorSexo(int eventoId)
        {
            var lista = Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null);

            return lista;
        }

        public int ContarVagasUtilizadas(int eventoId)
        {
            var quantidade = Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null).Count();

            return quantidade;
        }

        public int ContarInscritos(int eventoId)
        {
            var quantidade = Db.Participantes.Where(p => p.EventoId == eventoId && p.DataCancelamento == null).Count();
            return quantidade;
        }

        public void RegistrarEmissaoCracha(int id)
        {
            Db.Participantes.Find(id).Cracha = true;
            Db.SaveChanges();
        }

        public void RegistrarEmissaoCertificado(int id)
        {
            Db.Participantes.Find(id).Certificado = true;
            Db.SaveChanges();
        }

    }
}
