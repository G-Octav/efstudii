using System.Data.Entity;

namespace EFStudiiCaz.StudiuCaz4
{
    public class BusinessContext : DbContext
    {
        public DbSet<Business> Businesses { get; set; }
        public BusinessContext() : base("name=EFStudiiCaz")
        { }
    }
}