using System.Collections.Generic;

namespace Facilis.Domain.Entities
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual IEnumerable<Cidade> Cidade { get; set; }
        public virtual int CidadeId {get; set;}
    }
}
