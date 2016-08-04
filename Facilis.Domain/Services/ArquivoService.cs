using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class ArquivoService : ServiceBase<Arquivo>, IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoService(IArquivoRepository arquivoRepository)
            : base(arquivoRepository)
        {
            _arquivoRepository = arquivoRepository;
        }

        public IEnumerable<Arquivo> ListarPorEvento(int eventoId)
        {
            return _arquivoRepository.ListarPorEvento(eventoId);
        }
    }
}
