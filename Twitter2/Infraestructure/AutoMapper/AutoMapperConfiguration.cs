using AutoMapper;
using Database.Model;
using DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twitter2.ViewModels;

namespace Twitter2.Infraestructure.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            ConfigureUser();
            ConfigurePubl();
            ConfigureHome();
            ConfigureComm();
            ConfigureAmigo();
        }
        private void ConfigureHome()
        {
            CreateMap<THomeViewModel, THomeViewModel>().ReverseMap();
        }
        private void ConfigureUser()
        {
            CreateMap<RegisterViewModel, UserDTO>().ReverseMap()
               .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.Ignore());
            CreateMap<UserDTO, Usuario>().ReverseMap();
        }
        private void ConfigurePubl()
        {
            CreateMap<Publicaciones, PublicacionViewModel>().ReverseMap();
            CreateMap<PublicacionViewModel, PublicacionDTO>().ReverseMap();
            CreateMap<PublicacionDTO, Publicaciones>().ReverseMap();
        }

        private void ConfigureComm()
        {
            CreateMap<ComentarioViewModel, ComentarioDTO>().ReverseMap();
            CreateMap<ComentarioDTO, Comentarios>().ReverseMap();
            CreateMap<RepliesDTO, Replies>().ReverseMap();
        }
        private void ConfigureAmigo()
        {
            CreateMap<Amigos, AmigosDTO>().ReverseMap();
        }
    }
}
