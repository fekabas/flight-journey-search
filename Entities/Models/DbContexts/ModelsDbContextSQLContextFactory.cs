using DataLayer.Configuration;
using Entities.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Entities.Models.DbContexts;

public class ModelsDbContextSQLContextFactory : IDesignTimeDbContextFactory<ModelsDbContextSQL>
{
    public ModelsDbContextSQL CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = DbContextFactoryHelper.GetConfiguration();

        DbContextOptionsBuilder<ModelsDbContextSQL> optionsBuilder = new();

        DatabaseConfiguration dbConfig = new DatabaseConfiguration().Bind(configuration);

        optionsBuilder.UseSqlServer(dbConfig.ModelConnection);

        return new ModelsDbContextSQL(optionsBuilder.Options);
    }
}