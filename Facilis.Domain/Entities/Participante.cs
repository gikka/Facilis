using System;

namespace Facilis.Domain.Entities
{
    public class Participante
    {
        public int ParticipanteId { get; set; }
        public bool Cracha { get; set; }
        public bool Brinde { get; set; }
        public bool Certificado { get; set; }
        public bool Presenca { get; set; }
        public DateTime DataCancelamento { get; set; }
        public DateTime DataInscricao { get; set; }

        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int EventoId{ get; set; }
        public virtual Evento Evento { get; set; }

        public int CupomId { get; set; }
        public virtual Cupom Cupom { get; set; }

    }
}
