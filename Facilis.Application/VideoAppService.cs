using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Facilis.Application
{
    public class VideoAppService : AppServiceBase<Video>, IVideoAppService
    {
        private readonly IVideoService _videoService;
        public VideoAppService(IVideoService videoService)
            :base(videoService)
        {
            _videoService = videoService;
        }

        public IEnumerable<Video> ListarPorEvento(int eventoId)
        {
            return _videoService.ListarPorEvento(eventoId);
        }
    }
}
