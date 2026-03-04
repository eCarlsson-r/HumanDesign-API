using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Infrastructure.Entities.Reference;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public new DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<Prospect> Prospects => Set<Prospect>();
    public DbSet<Design> Designs => Set<Design>();
    public DbSet<FileEntity> Files => Set<FileEntity>();

    public DbSet<AttributeEntity> Attributes => Set<AttributeEntity>();
    public DbSet<TypeEntity> Types => Set<TypeEntity>();
    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    public DbSet<CenterEntity> Centers => Set<CenterEntity>();
    public DbSet<GateEntity> Gates => Set<GateEntity>();
    public DbSet<ChannelEntity> Channels => Set<ChannelEntity>();
    public DbSet<CrossEntity> Crosses => Set<CrossEntity>();
    public DbSet<DefinedChannel> DefinedChannels => Set<DefinedChannel>();
    public DbSet<PlanetaryActivation> PlanetaryActivations => Set<PlanetaryActivation>();
    public DbSet<CenterDefinition> CenterDefinitions => Set<CenterDefinition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CenterDefinition>()
            .HasIndex(c => new { c.DesignId, c.CenterName })
            .IsUnique();

        modelBuilder.Entity<CenterEntity>()
            .HasIndex(c => new { c.CenterName, c.Definition })
            .IsUnique();
    }
}