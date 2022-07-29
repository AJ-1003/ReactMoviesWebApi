using AutoMapper;
using ReactMoviesWebApi.DTO;
using ReactMoviesWebApi.Entities;

namespace ReactMoviesWebApi.Profiles
{
    public class GenresProfile : Profile
    {
        public GenresProfile()
        {
            CreateMap<Genre, GenreDTO>()
                .ReverseMap();
            CreateMap<CreateUpdate_GenreDTO, Genre>()
                .ForMember(g => g.Id, options => options.Ignore())
                .ReverseMap();
        }
    }
}
