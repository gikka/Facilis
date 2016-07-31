using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class CidadeService :  ServiceBase<Cidade> , ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
            : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        public IEnumerable<Cidade> ListarPorEstado(int estadoId)
        {
            return _cidadeRepository.ListarPorEstado(estadoId);
        }
    }
}
