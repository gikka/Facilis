using Facilis.Infra.CrossCutting.Identity.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace Facilis.MVC.ViewModels
{
    public class CupomViewModel
    {
        [Key]
        public int CupomId { get; set; }
        
        [Required(ErrorMessage = "Preencha o campo Nome do cupom")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [Display(Name = "Nome do cupom")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "Preencha o campo % Desconto")]
        [Display(Name = "% Desconto")]
        public int PercentualDesconto { get; set; }

        [Required(ErrorMessage ="Preencha o campo Quantidade")]
        [Display(Name ="Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Preencha o campo Validade até")]
        [Display(Name = "Validade até")]
        [DataType(DataType.Date)]
        public DateTime Validade { get; set; }

        [Required(ErrorMessage = "Preencha o campo Evento")]
        [Display(Name = "Evento")]
        public int EventoId { get; set; }
        public virtual EventoViewModel Evento { get; set; }

        public string UsuarioId { get; set; }
        public virtual RegisterViewModel Usuario { get; set; }
    }
}