namespace Facilis.MVC.ViewModels
{
    public class CidadeViewModel
    {

        public int CidadeId { get; set; }
        public string Nome { get; set; }
        public int EstadoId { get; set; }

        public virtual EstadoViewModel Estado { get; set; }

    }
}