using MergeInMemory.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Tab.Toolkit.Testing.Common;

public static class TestDatabaseHelper
{
    public static TestSetupConfiguration CreateDatabase(string dbName)
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
        .UseInMemoryDatabase(dbName);
        
        return new TestSetupConfiguration(dbName, options.Options);
    }

    public static MyDbContext CreateContext(TestSetupConfiguration testSetupConfiguration)
    {
        return new MyDbContext(
            testSetupConfiguration.DbContextOptions
        )
        { DatabaseNameForTestingOnly = testSetupConfiguration.DatabaseName };
    }
}