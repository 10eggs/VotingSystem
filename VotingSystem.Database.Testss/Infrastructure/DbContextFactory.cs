using Microsoft.EntityFrameworkCore;

namespace VotingSystem.Database.Testss.Infrastructure
{
    class DbContextFactory
    {
        public static  AppDbContext Create(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(nameof(dbName))
                .Options;

            return new AppDbContext(options);
        }
    }
}
