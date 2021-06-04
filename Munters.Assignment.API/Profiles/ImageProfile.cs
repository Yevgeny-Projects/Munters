using Munters.Assignment.Entities;
using Munters.Assignment.Shared.DTO;

namespace Munters.Assignment.API.Profiles
{
    public class ImageProfile : AutoMapper.Profile
    {
        public ImageProfile()
        {
            CreateMap<Datum, ImageResponse>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.id))
                .ForMember(x => x.Url, opt => opt.MapFrom(src => src.url));
            ;
        }
    }
}
