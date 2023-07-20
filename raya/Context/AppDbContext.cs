
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Nurse>().HasKey(nurse => nurse.Id);
        modelBuilder.Entity<Nurse>().Property(nurse=>nurse.NurseCategory).HasConversion<string>();
        modelBuilder.Entity<ReserveNurse>().HasKey(reserveNurse => reserveNurse.Id);
        modelBuilder.Entity<ReserveNurse>().HasOne(_ => _.Nurse).WithMany(_ => _.ReserveNurses).HasForeignKey(_ => _.NurseId);
        modelBuilder.Entity<ReserveNurse>().HasOne(_ => _.UserReserved).WithMany(_ => _.ReserveNurses).HasForeignKey(_ => _.UserId);
        modelBuilder.Entity<ClassCategory>().HasOne(_ => _.Class).WithMany(_ => _.ClassCategories).HasForeignKey(_ => _.ClassId);
        modelBuilder.Entity<Message>().HasOne(_ => _.User).WithMany(_ => _.Messages).HasForeignKey(_ => _.UserId);
        modelBuilder.Entity<ReserveClass>().HasOne(_ => _.UserReserved).WithMany(_ => _.ReservedClasses).HasForeignKey(_ => _.UserId);
        modelBuilder.Entity<ReserveClass>().HasOne(_ => _.ClassCategory).WithMany(_ => _.ReserveClasses).HasForeignKey(_ => _.ClassCategoryId);

    }

    public override int SaveChanges()
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            entry.Property("UpdatedAt").CurrentValue = now;

            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedAt").CurrentValue = now;
            }
        }
        return base.SaveChanges();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Nurse> Nurses { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassCategory> ClassCategories { get; set; }
    public DbSet<ReserveNurse> ReserveNurses { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ReserveClass> ReserveClasses { get; set; }
}