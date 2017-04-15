using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MovieHub.Models;
using MovieHub.Models.DTOs;
using MovieHub.ViewModels.Account;
using System.IO;
using MovieHub.Data;
using MovieHub.ViewModels.Movie;

namespace MovieHub.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(action =>
            {
                action.CreateMap<Movie, MovieDTO>();

                action.CreateMap<RegisterViewModel, ApplicationUser>()
                    .ForMember(dest => dest.UserName, cfg => cfg.MapFrom(src => src.Username));
                // .ForMember(dest => dest.ProfilePicture, cfg => cfg.MapFrom(src => ConvertToByteArray(src.ProfilePicture)));

                action.CreateMap<string, Actor>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Genre>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Director>()
               .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                //action.CreateMap<MovieViewModel, MovieDTO>()
                //.ForMember(dest => dest.Runtime, mo => mo.MapFrom(src => src.Runtime.ToString()));

                //action.CreateMap<MovieDTO, Movie>()
                //.ForMember(dest => dest.Genres, cfg => cfg.MapFrom(src => MovieDbContext.GetGenresByName(context, src.Genres)));
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