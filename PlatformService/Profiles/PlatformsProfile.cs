using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // mapping from platformcreatedto to platform
            CreateMap<PlatformCreateDto, Platforms>();//.ReverseMap(); <- enables reverse mapping

            // mapping for platform to platform read dto
            CreateMap<Platforms, PlatformReadDto>();
        }
    }
}