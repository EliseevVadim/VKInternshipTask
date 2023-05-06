using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKInternshipTask.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(UsersAPIDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
