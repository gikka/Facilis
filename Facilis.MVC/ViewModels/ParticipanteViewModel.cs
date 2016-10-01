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

        [Display(Name = "Crachá")]
        public bool Cracha { get; set; }
        public bool Brinde { get; set; }

        public bool Certificado { get; set; }

        [Display(Name = "Presença")]
        public bool Presenca { get; set; }

        [Display(Name = "Data de cancelamento")]
        [DataType(DataType.DateTime)]
        public DateTime ?DataCancelamento { get; set; }

        [Display(Name = "Data de inscrição")]
        [DataType(DataType.DateTime)]
        public DateTime DataInscricao { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public int ?FormaPagamento { get; set; }

        public string UsuarioId { get; set; }
        public virtual RegisterViewModel Usuario { get; set; }

        [Display(Name = "Evento")]
        public int EventoId { get; set; }
        public virtual EventoViewModel Evento { get; set; }

        public int? CupomId { get; set; }
        public virtual CupomViewModel CupomViewModel { get; set; }

        //****usado apenas nos relatórios e gráficos
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        public IEnumerable<EstadoViewModel>Estado { get; set; }
        public IEnumerable<CidadeViewModel> Cidade { get; set; }
        //****
    }
}