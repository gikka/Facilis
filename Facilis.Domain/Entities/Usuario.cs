﻿using System;
using System.Collections.Generic;

namespace Facilis.Domain.Entities
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }        
        public DateTime DataNascimento { get; set; }
        public int Telefone { get; set; }
        public string Sexo { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public int Cep { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado {get; set;}
        public int CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }
        public virtual IEnumerable<Evento> Eventos { get; set; }
        public virtual IEnumerable<Cupom> Cupons { get; set; }
        public virtual IEnumerable<Participante> Participantes { get; set; }
    }
}
