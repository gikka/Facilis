using Facilis.Domain.Entities;
using System.Collections.Generic;

namespace Facilis.Application.Interface
{
    public interface IVideoAppService : IAppServiceBase<Video>
    {
        IEnumerable<Video> ListarPorEvento(int eventoId);
    }
}
