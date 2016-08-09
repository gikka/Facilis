using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Services
{
    public interface IVideoService : IServiceBase<Video>
    {
        IEnumerable<Video> ListarPorEvento(int eventoId);
    }
}
