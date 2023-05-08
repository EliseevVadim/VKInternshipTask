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
