using MergeInMemory.Tests.Setup;
using Microsoft.EntityFrameworkCore;
using Tab.Toolkit.Testing.Common;

namespace MergeInMemory.Tests;

public class UnitTest2
{
    private readonly MyDbContext _context;

    public UnitTest2()
    {
        var testSetupConfiguration = TestDatabaseHelper.CreateDatabase(nameof(UnitTest2));
        GlobalTestFixture.AddTestSetupConfiguration(testSetupConfiguration);
        _context = TestDatabaseHelper.CreateContext(testSetupConfiguration);
    }
    
    [Fact]
    public void Test2()
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
            new MyEntity() { Name = "six2" },
            new MyEntity() { Name = "seven2" },
            new MyEntity() { Name = "eight2" },
            new MyEntity() { Name = "nine2" },
            new MyEntity() { Name = "ten2" },
        };
        _context.MyEntities.BulkMerge(entities2, options =>
        {
            options.ColumnPrimaryKeyExpression = o => o.Name;
            options.IgnoreOnMergeUpdateExpression = o => o.Id;
        });
        
        Assert.Equal(20, _context.MyEntities.AsNoTracking().Count());
    }
}