using System;

namespace Facilis.Domain.Entities
{
    public class Evento
    {
        public int EventoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicial { get; set; }
        public TimeSpan HoraInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public TimeSpan HoraFinal { get; set; }
        public int QuantidadeVagas { get; set; }
        public string NomePalestrante { get; set; }
        public string EmailPalestrante { get; set; }
        public int TipoEvento { get; set; }
        public string Descricao { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
