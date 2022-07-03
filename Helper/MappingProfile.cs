using AutoMapper;
using MoviesApi.Dtos;
using MoviesApi.Models;

namespace MoviesApi.Helper
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
    {
        CreateMap<Moive, MoivesDetailsDto>();
        CreateMap<MoivesInputDto, Moive>()
            .ForMember(src => src.Poster, opt => opt.Ignore());
    }
}
}
