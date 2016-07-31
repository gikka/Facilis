using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;

namespace Facilis.Domain.Services
{
    public class EstadoService : ServiceBase<Estado>, IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;

        public EstadoService(IEstadoRepository estadoRepository)
            : base(estadoRepository)
        {
            _estadoRepository = estadoRepository;
        }
    }
}
