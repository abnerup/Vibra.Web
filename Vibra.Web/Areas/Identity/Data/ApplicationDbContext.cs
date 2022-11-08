using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vibra.Web.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Revenue> Revenues { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Categorie> Categories { get; set; }

    public DbSet<Customer> Customers { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        builder.ApplyConfiguration(new ExpenseEntityConfiguration());

        builder.ApplyConfiguration(new RevenueEntityConfiguration());
    }
}

public class ExpenseEntityConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.Property(d => d.Amount).HasColumnType("decimal(5 , 2)");
        
    }
}

public class RevenueEntityConfiguration : IEntityTypeConfiguration<Revenue>
{
    public void Configure(EntityTypeBuilder<Revenue> builder)
    {
        builder.Property(d => d.Amount).HasColumnType("decimal(5 , 2)");
    }
}


public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(d => d.FirstName).HasMaxLength(50);
        builder.Property(d => d.LastName).HasMaxLength(50);
    }
}