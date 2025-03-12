using Microsoft.EntityFrameworkCore;

namespace FinalLabTask1.Entities;

public class TransactionDbContext: DbContext
{
    public DbSet<TransactionLogs> TransactionLogs { get; set; }
    public DbSet<AccountTransactions> AccountTransactions { get; set; }

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TransactionLogs>().HasKey(x => x.Id);
        modelBuilder.Entity<AccountTransactions>().HasKey(x => x.Id);
        modelBuilder.HasDefaultSchema("public");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=transaction_schema;Username=ALIJAD;Password=alijad");
}