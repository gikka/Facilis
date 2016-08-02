using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class CupomService : ServiceBase<Cupom>, ICupomService
    {
        private readonly ICupomRepository _cupomRepository;

        public CupomService(ICupomRepository cupomRepository)
            : base(cupomRepository)
        {
            _cupomRepository = cupomRepository;
        }

        public IEnumerable<Cupom> ListarPorUsuario(string usuarioId)
        {
            return _cupomRepository.ListarPorUsuario(usuarioId);
        }
    }
}
