using AutoMapper;
using Facilis.Domain.Entities;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.MVC.ViewModels;

namespace Facilis.MVC.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<EventoViewModel, Evento>();
            Mapper.CreateMap<RegisterViewModel, Usuario>();
            Mapper.CreateMap<CupomViewModel, Cupom>();
            Mapper.CreateMap<ArquivoViewModel, Arquivo>();
            Mapper.CreateMap<VideoViewModel, Video>();
            Mapper.CreateMap<ParticipanteViewModel, Participante>();
        }
    }
}