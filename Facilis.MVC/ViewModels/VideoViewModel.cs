using System.ComponentModel.DataAnnotations;

namespace Facilis.MVC.ViewModels
{
    public class VideoViewModel
    {
        [Key]
        public int VideoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Título")]
        [Display(Name = "Título")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Preencha o campo URL")]
        [Display(Name = "URL do vídeo")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string URL { get; set; }

        public int EventoId { get; set; }
        public virtual EventoViewModel Evento { get; set; }
    }
}