﻿using Facilis.Application;
using Facilis.Application.Interface;
using Facilis.Domain.Interfaces.Repositories;
using Facilis.Domain.Interfaces.Services;
using Facilis.Domain.Services;
using Facilis.Infra.CrossCutting.Identity.Configuration;
using Facilis.Infra.CrossCutting.Identity.Model;
using Facilis.Infra.Data.Context;
using Facilis.Infra.Data.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;

namespace Facilis.Infra.CrossCutting.IoC
{
    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.RegisterPerWebRequest<FacilisContext>();
            container.RegisterPerWebRequest<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>(new FacilisContext()));
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>>(() => new RoleStore<IdentityRole>());
            //container.RegisterPerWebRequest<ApplicationRoleManager>();
            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();

            container.RegisterPerWebRequest<IEventoAppService, EventoAppService>();
            container.RegisterPerWebRequest<IEventoService, EventoService>();
            container.RegisterPerWebRequest<IEventoRepository, EventoRepository>();

            container.RegisterPerWebRequest<IEstadoAppService, EstadoAppService>();
            container.RegisterPerWebRequest<IEstadoService, EstadoService>();
            container.RegisterPerWebRequest<IEstadoRepository, EstadoRepository>();

            container.RegisterPerWebRequest<ICidadeAppService, CidadeAppService>();
            container.RegisterPerWebRequest<ICidadeService, CidadeService>();
            container.RegisterPerWebRequest<ICidadeRepository, CidadeRepository>();

            container.RegisterPerWebRequest<ICupomAppService, CupomAppService>();
            container.RegisterPerWebRequest<ICupomService, CupomService>();
            container.RegisterPerWebRequest<ICupomRepository, CupomRepository>();
            
            container.RegisterPerWebRequest<IArquivoAppService, ArquivoAppService>();
            container.RegisterPerWebRequest<IArquivoService, ArquivoService>();
            container.RegisterPerWebRequest<IArquivoRepository, ArquivoRepository>();

            container.RegisterPerWebRequest<IVideoAppService, VideoAppService>();
            container.RegisterPerWebRequest<IVideoService, VideoService>();
            container.RegisterPerWebRequest<IVideoRepository, VideoRepository>();

            container.RegisterPerWebRequest<IParticipanteAppService, ParticipanteAppService>();
            container.RegisterPerWebRequest<IParticipanteService, ParticipanteService>();
            container.RegisterPerWebRequest<IParticipanteRepository, ParticipanteRepository>();
        }
    }
}