using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReactMoviesWebApi.DTO;

namespace ReactMoviesWebApi.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // CreateMap<TSource, TDestination>
            CreateMap<IdentityUser, UserDTO>();
        }
    }
}
