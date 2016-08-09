using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Facilis.Infra.Data.Repositories
{
    public class VideoRepository : RepositoryBase<Video>, IVideoRepository
    {
        public IEnumerable<Video> ListarPorEvento(int eventoId)
        {
            return Db.Videos.Where(v => v.EventoId == eventoId);
        }
    }
}
