using System;

namespace Facilis.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public int Telefone { get; set; }
        public string Sexo { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public int Cep { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }

    }
}
