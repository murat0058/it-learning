using ITLearning.Frontend.Web.Common.Configs;
using Microsoft.Data.Entity;
using Microsoft.Framework.OptionsModel;

namespace ITLearning.Frontend.Web.DAL
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