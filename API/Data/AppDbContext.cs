using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
public class CalculationDBContext : IdentityDbContext<ApplicationUser>
{
    public CalculationDBContext(DbContextOptions<CalculationDBContext> options) : base(options)
    {
        
    }
}