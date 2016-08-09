using Facilis.Infra.CrossCutting.Identity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facilis.MVC.ViewModels
{
    public class EventoViewModel
    {
        [Key]
        public int EventoId { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome do evento")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [Display(Name = "Nome do evento")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Data inicial")]
        [Display(Name = "Data inicial")]
        [DataType(DataType.Date)]
        public DateTime DataInicial { get; set; }

        [Required(ErrorMessage = "Preencha o campo Hora inicial")]
        [Display(Name = "Hora inicial")]
        [DataType(DataType.Time)]
        public TimeSpan HoraInicial { get; set; }

        [Required(ErrorMessage = "Preencha o campo Data final")]
        [Display(Name = "Data final")]
        [DataType(DataType.Date)]
        public DateTime DataFinal { get; set; }

        [Required(ErrorMessage = "Preencha o campo Hora final")]
        [Display(Name = "Hora final")]
        [DataType(DataType.Time)]
        public TimeSpan HoraFinal { get; set; }

        [Required(ErrorMessage = "Preencha o campo Quantidade de vagas")]
        [Display(Name = "Quantidade de vagas")]
        public int QuantidadeVagas { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome do palestrante/instrutor")]
        [Display(Name = "Nome do palestrante/instrutor")]
        [MaxLength(200, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string NomePalestrante { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail do palestrante/instrutor")]
        [MaxLength(256, ErrorMessage = "Máximo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail válido")]
        [Display(Name = "E-mail do palestrante/instrutor")]
        public string EmailPalestrante { get; set; }

        [Required(ErrorMessage = "Preencha o campo Tipo do evento")]
        [Display(Name = "Tipo do evento")]
        public int TipoEvento { get; set; }

        [Required(ErrorMessage = "Preencha o campo Descrição do evento")]
        [Display(Name = "Descrição do evento")]
        [MaxLength(5000, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        public string UsuarioId { get; set; }
        public virtual RegisterViewModel Usuario { get; set; }

        public virtual IEnumerable<ArquivoViewModel> Arquivos { get; set; }
        public virtual IEnumerable<VideoViewModel> Videos { get; set; }
    }
}