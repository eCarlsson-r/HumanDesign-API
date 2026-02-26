using HumanDesign.Infrastructure.Entities;
using HumanDesign.Infrastructure.Entities.Charts;
using HumanDesign.Infrastructure.Entities.Reference;
using Microsoft.EntityFrameworkCore;

namespace HumanDesign.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Design> Designs => Set<Design>();
    public DbSet<Prospect> Prospects => Set<Prospect>();

    public DbSet<TypeEntity> Types => Set<TypeEntity>();
    public DbSet<ProfileEntity> Profiles => Set<ProfileEntity>();
    public DbSet<GateEntity> Gates => Set<GateEntity>();
    public DbSet<ChannelEntity> Channels => Set<ChannelEntity>();
    public DbSet<DefinedChannel> DefinedChannels => Set<DefinedChannel>();
    public DbSet<CrossEntity> Crosses => Set<CrossEntity>();
    public DbSet<PlanetaryActivation> PlanetaryActivations => Set<PlanetaryActivation>();
    public DbSet<AttributeEntity> Attributes => Set<AttributeEntity>();

    public DbSet<CenterEntity> CenterDefinitions => Set<CenterEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CenterEntity>()
            .HasIndex(c => new { c.DesignId, c.CenterName })
            .IsUnique();
    }
}