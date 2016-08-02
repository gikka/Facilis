using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Application
{
    public class CupomAppService : AppServiceBase<Cupom>, ICupomAppService
    {
        private readonly ICupomService _cupomService;

        public CupomAppService(ICupomService cupomService)
            : base(cupomService)
        {
            _cupomService = cupomService;
        }

        public IEnumerable<Cupom> ListarPorUsuario(string usuarioId)
        {
            return _cupomService.ListarPorUsuario(usuarioId);
        }
    }
}
