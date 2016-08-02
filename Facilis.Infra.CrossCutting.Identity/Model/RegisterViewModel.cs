using Facilis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Facilis.Infra.CrossCutting.Identity.Model
{
    public class RegisterViewModel
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = "Preencha o campo Nome")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Preencha o campo Sobrenome")]
        [MaxLength(100, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Preencha o campo E-mail")]
        [MaxLength(256, ErrorMessage = "Máximo {0} caracteres")]
        [EmailAddress(ErrorMessage = "Preencha um e-mail válido")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Preencha o campo Senha")]
        [MaxLength(20, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(8, ErrorMessage = "Mínimo {0} caracteres")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Preencha o campo Confirmar Senha")]
        [MaxLength(20, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(8, ErrorMessage = "Mínimo {0} caracteres")]
        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }

        [Required(ErrorMessage = "Preencha o campo Data de Nascimento")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Preencha o campo Telefone")]
        [DataType(DataType.PhoneNumber)]
        public int Telefone { get; set; }

        [Required(ErrorMessage = "Preencha o campo Sexo")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Preencha o campo Endereço")]
        [MaxLength(150, ErrorMessage = "Máximo {0} caracteres")]
        [MinLength(2, ErrorMessage = "Mínimo {0} caracteres")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Preencha o campo Número")]
        [MaxLength(10, ErrorMessage = "Máximo {0} caracteres")]
        [Display(Name = "Número")]
        public string Numero { get; set; }


        [Required(ErrorMessage = "Preencha o campo Cep")]
        [DataType(DataType.PostalCode)]
        public int Cep { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo {0} caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Preencha o campo Bairro")]
        [MaxLength(50, ErrorMessage = "Máximo {0} caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Preencha o campo Estado")]
        [Display(Name = "Estado")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }

        [Required(ErrorMessage = "Preencha o campo Cidade")]
        [Display(Name = "Cidade")]
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual IEnumerable<Evento> Eventos { get; set; }
    }
}
