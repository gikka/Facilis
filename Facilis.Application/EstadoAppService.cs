using Facilis.Application.Interface;
using Facilis.Domain.Entities;
using Facilis.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facilis.Application
{
    public class EstadoAppService : AppServiceBase<Estado>, IEstadoAppService
    {
        private readonly IEstadoService _estadoService;

        public EstadoAppService(IEstadoService estadoService)
            :base(estadoService)
        {
            _estadoService = estadoService;
        }
    }
}
