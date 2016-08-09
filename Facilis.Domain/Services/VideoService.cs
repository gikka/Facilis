using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Domain.Services
{
    public class VideoService : ServiceBase<Video>, IVideoService 
    {
        private readonly IVideoRepository _videoRepository;

        public VideoService(IVideoRepository videoRepository)
            : base(videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IEnumerable<Video> ListarPorEvento(int eventoId)
        {
            return _videoRepository.ListarPorEvento(eventoId);
        }
    }
}
