using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilis.Domain.Entities
{
    public class Email
    {
        public string HtmlContent { get; set; }
        public string Assunto { get; set; }

        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }

    }
}
