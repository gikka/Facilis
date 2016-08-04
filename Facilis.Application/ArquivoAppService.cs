using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Application
{
    public class ArquivoAppService : AppServiceBase<Arquivo>, IArquivoAppService
    {
        private readonly IArquivoService _arquivoService;
        public ArquivoAppService(IArquivoService arquivoService)
            :base(arquivoService)
        {
            _arquivoService = arquivoService;
        }

        public IEnumerable<Arquivo> ListarPorEvento(int eventoId)
        {
            return _arquivoService.ListarPorEvento(eventoId);
        }
    }
}
