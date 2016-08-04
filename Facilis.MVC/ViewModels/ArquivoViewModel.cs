using System.ComponentModel.DataAnnotations;

namespace Facilis.MVC.ViewModels
{
    public class ArquivoViewModel
    {
        [Key]
        public int ArquivoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Título do arquivo")]
        [Display(Name = "Título do arquivo")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Descrição")]
        [Display(Name = "Descrição")]
        [MaxLength(250, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public int EventoId { get; set; }
        public virtual EventoViewModel Evento { get; set; }
    }
}