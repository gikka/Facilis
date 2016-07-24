using System.ComponentModel.DataAnnotations;

namespace Facilis.Infra.CrossCutting.Identity.Model
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
