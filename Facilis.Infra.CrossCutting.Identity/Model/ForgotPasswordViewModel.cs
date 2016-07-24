using System.ComponentModel.DataAnnotations;

namespace Facilis.Infra.CrossCutting.Identity.Model
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
