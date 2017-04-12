using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MovieHub.Models;
using MovieHub.Models.DTOs;

namespace MovieHub.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(action =>
            {
                action.CreateMap<Movie, MovieDto>();
            });
        }

    }
}