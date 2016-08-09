using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Domain.Interfaces.Repositories
{
    public interface IVideoRepository : IRepositoryBase<Video>
    {
        IEnumerable<Video> ListarPorEvento(int eventoId);
    }
}
