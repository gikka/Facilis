using System.Collections.Generic;

namespace Facilis.Domain.Entities
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual IEnumerable<Usuario> Usuarios { get; set; }
    }
}
