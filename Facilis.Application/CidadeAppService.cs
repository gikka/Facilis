using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Application
{
    public class CidadeAppService : AppServiceBase<Cidade>, ICidadeAppService
    {
        private readonly ICidadeService _cidadeService;

        public CidadeAppService(ICidadeService cidadeService)
            : base(cidadeService)
        {
            _cidadeService = cidadeService;
        }

        public IEnumerable<Cidade> ListarPorEstado(int estadoId)
        {
            return _cidadeService.ListarPorEstado(estadoId);
        }
    }
}
