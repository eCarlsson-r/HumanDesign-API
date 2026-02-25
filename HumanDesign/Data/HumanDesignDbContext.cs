using HumanDesignApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanDesignApi.Data;

public class HumanDesignDbContext : DbContext
{
    public HumanDesignDbContext(DbContextOptions<HumanDesignDbContext> options)
        : base(options)
    {
    }

    public DbSet<Prospect> Prospects { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Design> Designs { get; set; }
    public DbSet<FileRecord> Files { get; set; }
    public DbSet<TypeEntity> Types { get; set; }
    public DbSet<AttributeEntity> Attributes { get; set; }
    public DbSet<IncarnationCrossEntity> IncarnationCrosses { get; set; }
    public DbSet<ProfileEntity> Profiles { get; set; }
    public DbSet<ChannelEntity> Channels { get; set; }
    public DbSet<GateEntity> Gates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Custom configurations if needed
    }
}
