using AutoMapper;
using NetTopologySuite.Geometries;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Profiles
{
    public class ActorsProfile : Profile
    {
        public ActorsProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<ActorDTO, Actor>()
                .ReverseMap();
            CreateMap<CreateUpdate_ActorDTO, Actor>()
                .ForMember(a => a.Id, options => options.Ignore())
                .ForMember(a => a.Picture, options => options.Ignore())
                .ReverseMap();
            CreateMap<Actor, ActorsMovieDTO>()
                .ForMember(a => a.Order, options => options.Ignore())
                .ForMember(a => a.Character, options => options.Ignore())
                .ReverseMap();
        }
    }
}
