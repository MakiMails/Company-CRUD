using Microsoft.EntityFrameworkCore;

namespace CRUDGrpcService.Models
{
    public class ApplicationContext :DbContext 
    {
        public DbSet<Company> Companys { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
