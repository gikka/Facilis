using System.Collections.Generic;

namespace Facilis.MVC.ViewModels
{
    public class EstadoViewModel
    {
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public virtual IEnumerable<CidadeViewModel> Cidades { get; set; }
    }
}