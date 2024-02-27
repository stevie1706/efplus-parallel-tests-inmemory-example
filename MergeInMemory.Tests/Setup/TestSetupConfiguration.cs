using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MergeInMemory.Tests.Setup;

public record TestSetupConfiguration(string DatabaseName, DbContextOptions<MyDbContext> DbContextOptions);