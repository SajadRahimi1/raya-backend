
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(user=>user.Id);
        modelBuilder.Entity<Nurse>().HasKey(nurse=>nurse.Id);
        modelBuilder.Entity<ReserveNurse>().HasKey(reserveNurse=>reserveNurse.Id);
        modelBuilder.Entity<ReserveNurse>().HasOne(_=>_.Nurse).WithMany(_=>_.ReserveNurses).HasForeignKey(_=>_.Id);
        modelBuilder.Entity<ClassCategories>().HasOne(_=>_.Class).WithMany(_=>_.ClassCategories).HasForeignKey(_=>_.Id);        
    }

    public DbSet<User> Users {get; set;}
    public DbSet<Nurse> Nurses {get; set;}
    public DbSet<Class> Classes { get; set; }
    public DbSet<ClassCategories> ClassCategories { get; set; }
    public DbSet<ReserveNurse> ReserveNurses { get; set; }
}