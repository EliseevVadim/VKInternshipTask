using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Persistence;

namespace VKInternshipTask.Tests.Common
{
    public abstract class TestBase : IDisposable
    {
        protected UsersAPIDbContext Context;
        protected IMapper Mapper;

        public TestBase()
        {
            Context = UsersAPIContextFactory.Create();
            var configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(IUsersAPIDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            UsersAPIContextFactory.Destroy(Context);
        }
    }
}
