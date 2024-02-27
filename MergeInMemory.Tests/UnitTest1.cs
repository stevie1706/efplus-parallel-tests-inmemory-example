using MergeInMemory.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using Tab.Toolkit.Testing.Common;
using Z.EntityFramework.Extensions;

namespace MergeInMemory.Tests;

public class UnitTest1
{
    private readonly MyDbContext _context;

    public UnitTest1()
    {
        var testSetupConfiguration = TestDatabaseHelper.CreateDatabase(nameof(UnitTest1));
        GlobalTestFixture.AddTestSetupConfiguration(testSetupConfiguration);
        _context = TestDatabaseHelper.CreateContext(testSetupConfiguration);
    }
    
    [Fact]
    public void Test1()
    {
        var entities = new List<MyEntity>
        {
            new MyEntity() { Name = "one" },
            new MyEntity() { Name = "two" },
            new MyEntity() { Name = "three" },
            new MyEntity() { Name = "four" },
            new MyEntity() { Name = "five" },
            new MyEntity() { Name = "six" },
            new MyEntity() { Name = "seven" },
            new MyEntity() { Name = "eight" },
            new MyEntity() { Name = "nine" },
            new MyEntity() { Name = "ten" },
        };
        _context.MyEntities.BulkMerge(entities, options =>
        {
            options.ColumnPrimaryKeyExpression = o => o.Name;
            options.IgnoreOnMergeUpdateExpression = o => o.Id;
        });
        
        Assert.Equal(10, _context.MyEntities.AsNoTracking().Count());
        
        var entities2= new List<MyEntity>
        {
            new MyEntity() { Name = "one2" },
            new MyEntity() { Name = "two2" },
            new MyEntity() { Name = "three2" },
            new MyEntity() { Name = "four2" },
            new MyEntity() { Name = "five2" },
            new MyEntity() { Name = "six" },
            new MyEntity() { Name = "seven" },
            new MyEntity() { Name = "eight" },
            new MyEntity() { Name = "nine" },
            new MyEntity() { Name = "ten" },
        };
        _context.MyEntities.BulkMerge(entities2, options =>
        {
            options.ColumnPrimaryKeyExpression = o => o.Name;
            options.IgnoreOnMergeUpdateExpression = o => o.Id;
        });
        
        Assert.Equal(15, _context.MyEntities.AsNoTracking().Count());
    }
}