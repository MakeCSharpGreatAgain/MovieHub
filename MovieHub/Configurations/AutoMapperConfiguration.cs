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
using MovieHub.ViewModels.User;
using MovieHub.ViewModels.Review;
using Microsoft.AspNet.Identity;

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

                action.CreateMap<string, Actor>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Genre>()
                .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));

                action.CreateMap<string, Director>()
               .ForMember<string>(dest => dest.Name, mo => mo.MapFrom(src => src));


                action.CreateMap<ApplicationUser, UserProfilePageViewModel>();

                action.CreateMap<Review, DeleteViewModel>()
                .ForMember(dest => dest.ReviewId, mo => mo.MapFrom(src => src.Id))
                .ForMember(dest => dest.MovieId, mo => mo.MapFrom(src => src.MovieId));

                action.CreateMap<Movie, ListAllViewModel>()
                .ForMember<int?>(dest => dest.ReleasedYear, mo => mo.MapFrom(src => src.Released != null ? (int?)src.Released.Value.Year : null));

                action.CreateMap<Review, DeleteViewModel>()
                .ForMember(dest => dest.ReviewId, mo => mo.MapFrom(src => src.Id));

                action.CreateMap<Review, EditViewModel>();
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