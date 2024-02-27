using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

namespace MergeInMemory.Tests.Setup;

public static class GlobalTestFixture
{
    private static List<TestSetupConfiguration> _testSetupConfigurations { get; set; }
    static GlobalTestFixture()
    {
        _testSetupConfigurations = new List<TestSetupConfiguration>();
        EntityFrameworkManager.ContextFactory = context =>
        {
            if (context is MyDbContext entityContext)
            {
                var dbName = entityContext.DatabaseNameForTestingOnly;
                var options = new DbContextOptionsBuilder<MyDbContext>()
                    .UseInMemoryDatabase(dbName);
        
                return new MyDbContext(options.Options);
            }
            return null;
        };
    }

    public static void AddTestSetupConfiguration(TestSetupConfiguration testSetupConfiguration)
    {
        if(_testSetupConfigurations.Any(x => x.DatabaseName == testSetupConfiguration.DatabaseName))
        {
            return;
        }
        _testSetupConfigurations.Add(new TestSetupConfiguration(testSetupConfiguration.DatabaseName, testSetupConfiguration.DbContextOptions));
    }

    private static TestSetupConfiguration GetTestSetupConfiguration(string databaseName)
    {
        return _testSetupConfigurations.First(x => x.DatabaseName == databaseName);
    }
}