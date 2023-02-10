using Microsoft.EntityFrameworkCore;


namespace BankAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<Person>? People { get; set; } = null;
        public DbSet<Currency>? Currency { get; set; } = null;
        public DbSet<BankAccount>? BankAccounts { get; set; } = null;
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlite("Data Source=mydb.db");
        }

    }
}
