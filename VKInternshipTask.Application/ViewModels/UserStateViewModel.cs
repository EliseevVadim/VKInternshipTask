using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Domain.Entities;

namespace VKInternshipTask.Application.ViewModels
{
    public class UserStateViewModel : IMapWith<UserState>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserState, UserStateViewModel>()
                .ForMember(vm => vm.Id, options => options.MapFrom(user => user.Id))
                .ForMember(vm => vm.Code, options => options.MapFrom(user => user.Code.ToString()))
                .ForMember(vm => vm.Description, options => options.MapFrom(user => user.Description));
        }
    }
}
