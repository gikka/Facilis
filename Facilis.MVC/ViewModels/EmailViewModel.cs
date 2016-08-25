using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Facilis.MVC.ViewModels
{
    public class EmailViewModel
    {
        [Required(ErrorMessage = "Preencha o campo Conteúdo do e-mail")]
        [AllowHtml]
        public string HtmlContent { get; set; }

        [Required(ErrorMessage = "Preencha o campo Assunto")]
        public string Assunto { get; set; }

        public int EventoId { get; set; }
        public virtual EventoViewModel EventoViewModel { get; set; }
        public virtual IEnumerable<ParticipanteViewModel> ParticipanteViewModel { get; set; }
    }
}