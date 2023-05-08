using AutoMapper;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.ViewModels
{
    public class UserGroupViewModel : IMapWith<UserGroup>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserGroup, UserGroupViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(user => user.Id))
                .ForMember(vm => vm.Code, options => options.MapFrom(user => user.Code.ToString()))
                .ForMember(vm => vm.Description, options => options.MapFrom(user => user.Description));
        }
    }
}
