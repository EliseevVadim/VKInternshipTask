using AutoMapper;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Application.Features.Users.Queries.GetUsers;

namespace VKInternshipTask.WebApi.Dto
{
    public class GetUsersDto : IMapWith<GetUsersQuery>
    {
        public int? Page { get; set; }
        public int? Size { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<GetUsersDto, GetUsersQuery>()
                .ForMember(query => query.Page, options => options.MapFrom(dto => dto.Page))
                .ForMember(query => query.Size, options => options.MapFrom(dto => dto.Size));
        }
    }
}
