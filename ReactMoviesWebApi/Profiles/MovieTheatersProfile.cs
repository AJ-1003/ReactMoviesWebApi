using AutoMapper;
using NetTopologySuite.Geometries;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Profiles
{
    public class MovieTheatersProfile : Profile
    {
        public MovieTheatersProfile(GeometryFactory geometryFactory)
        {
            // CreateMap<TSource, TDestination>
            CreateMap<MovieTheater, MovieTheaterDTO>()
               .ForMember(mty => mty.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
               .ForMember(mtx => mtx.Longitude, dto => dto.MapFrom(prop => prop.Location.X));
            CreateMap<CreateUpdate_MovieTheaterDTO, MovieTheater>()
                .ForMember(mt => mt.Id, options => options.Ignore())
                .ForMember(mt => mt.Location, loc => loc.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
        }
    }
}
