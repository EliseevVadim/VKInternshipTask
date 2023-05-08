using AutoMapper;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.ViewModels
{
    public class UserViewModel : IMapWith<User>
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserGroupViewModel UserGroup { get; set; }
        public UserStateViewModel UserState { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(user => user.Id))
                .ForMember(vm => vm.Login, options => options.MapFrom(user => user.Login))
                .ForMember(vm => vm.CreatedDate, options => options.MapFrom(user => user.CreatedDate))
                .ForMember(vm => vm.UserGroup, options => options.MapFrom(user => user.UserGroup))
                .ForMember(vm => vm.UserState, options => options.MapFrom(user => user.UserState));
        }
    }
}
