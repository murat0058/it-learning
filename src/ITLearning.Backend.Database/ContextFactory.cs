using ITLearning.Shared.Configs;
using Microsoft.Data.Entity;
using Microsoft.Extensions.OptionsModel;

namespace ITLearning.Backend.Database
{
    public static class ContextFactory
    {
        public static AppDbContext GetDbContext(IOptions<DatabaseConfiguration> dbConfiguration)
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(dbConfiguration.Value.ConnectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}