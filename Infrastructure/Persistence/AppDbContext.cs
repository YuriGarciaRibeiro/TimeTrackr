namespace Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<DailyTimeRecord> TimeRecords => Set<DailyTimeRecord>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.FullName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(u => u.Email)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasIndex(u => u.Email)
                .IsUnique();

            entity.Property(u => u.PasswordHash)
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
        });

        modelBuilder.Entity<DailyTimeRecord>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.EmployeeId)
                .IsRequired();

            entity.Property(r => r.Date)
                .IsRequired();

            entity.Property(r => r.StartWork);
            entity.Property(r => r.StartLunch);
            entity.Property(r => r.EndLunch);
            entity.Property(r => r.EndWork);

            entity.HasOne(r => r.Employee)
                .WithMany(u => u.TimeRecords)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(r => new { r.EmployeeId, r.Date })
                .IsUnique();
        });
    }
}
