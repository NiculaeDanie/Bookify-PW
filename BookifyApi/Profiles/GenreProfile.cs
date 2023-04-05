using AutoMapper;
using Bookify.Dto;
using Domain;

namespace BookifyApi.Profiles
{
    public class GenreProfile: Profile
    {
        public GenreProfile()
        {
            CreateMap<GenrePutPostDto, Genre>();
            CreateMap<Genre, GenreGetDto>();
        }
    }
}
