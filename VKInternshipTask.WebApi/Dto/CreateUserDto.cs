using AutoMapper;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Application.Features.Users.Commands.CreateUser;

namespace VKInternshipTask.WebApi.Dto
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string UserGroupCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(command => command.Login, options => options.MapFrom(dto => dto.Login))
                .ForMember(command => command.Password, options => options.MapFrom(dto => dto.Password))
                .ForMember(command => command.UserGroupCode, options => options.MapFrom(dto => dto.UserGroupCode));
        }
    }
}
