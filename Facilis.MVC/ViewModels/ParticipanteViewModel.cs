using Facilis.Infra.CrossCutting.Identity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Facilis.MVC.ViewModels
{
    public class ParticipanteViewModel
    {
        [Key]
        public int ParticipanteId { get; set; }
        public bool Cracha { get; set; }
        public bool Brinde { get; set; }
        public bool Certificado { get; set; }
        public bool Presenca { get; set; }
        public DateTime DataCancelamento { get; set; }
        public DateTime DataInscricao { get; set; }

        public string UsuarioId { get; set; }
        public virtual RegisterViewModel UsuarioViewModel { get; set; }

        public int? EventoId { get; set; }
        public virtual EventoViewModel EventoViewModel { get; set; }

        public int? CupomId { get; set; }
        public virtual CupomViewModel CupomViewModel { get; set; }
    }
}