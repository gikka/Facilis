using System;
using System.Collections.Generic;

namespace Facilis.Domain.Entities
{
    public class Cupom
    {
        public int CupomId { get; set; }
        public string Nome { get; set; }
        public int PercentualDesconto { get; set; }
        public int Quantidade { get; set; }
        public DateTime Validade { get; set; }
        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual IEnumerable<Participante> Participantes { get; set; }
    }
}
