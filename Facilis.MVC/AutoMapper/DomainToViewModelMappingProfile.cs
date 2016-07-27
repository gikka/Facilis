using AutoMapper;
using Facilis.MVC.ViewModels;
using Facilis.Domain.Entities;

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
        }
    }
}