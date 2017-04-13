using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MovieHub.Models;
using MovieHub.Models.DTOs;
using MovieHub.ViewModels.Account;
using System.IO;

namespace MovieHub.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(action =>
            {
                action.CreateMap<Movie, MovieDto>();

                action.CreateMap<RegisterViewModel, ApplicationUser>()
                    .ForMember(dest => dest.UserName, cfg => cfg.MapFrom(src => src.Username));
               // .ForMember(dest => dest.ProfilePicture, cfg => cfg.MapFrom(src => ConvertToByteArray(src.ProfilePicture)));

                action.CreateMap<string, Actor>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Genre>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Director>()
               .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));
            });
        }

        private static byte[] ConvertToByteArray(HttpPostedFileBase file)
        {
            MemoryStream memoryStream = new MemoryStream();
            file.InputStream.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}