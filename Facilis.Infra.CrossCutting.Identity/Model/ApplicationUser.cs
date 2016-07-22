using Facilis.Domain.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Facilis.Infra.CrossCutting.Identity.Model
{
    public class ApplicationUser : IdentityUser
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
