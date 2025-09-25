using Microsoft.EntityFrameworkCore;

namespace DiningWheels.Persistance;

public class DiningWheelsDbContextFactory : DesignTimeDbContextFactoryBase<DiningWheelsDbContext>
{
    protected override DiningWheelsDbContext CreateNewInstance(DbContextOptions<DiningWheelsDbContext> options)
    {
        return new DiningWheelsDbContext(options);
    }
}