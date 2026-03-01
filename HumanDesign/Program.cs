using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Infrastructure.Persistence.Seed;
using HumanDesign.Application.Services.Reference;
using HumanDesign.Application.Services.Reports;
using HumanDesign.Application.Services.Processing;
using HumanDesign.Application.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IContentResolverService, ContentResolverService>();
builder.Services.AddScoped<IAIContentGenerator, OpenAIContentGenerator>();
builder.Services.AddScoped<ReferenceDataSeeder>();
builder.Services.AddScoped<IProspectService, ProspectService>(); 
builder.Services.AddScoped<FileResolver>();
builder.Services.AddScoped<IHumanDesignCalculator, HumanDesignCalculator>();
builder.Services.AddScoped<IVariableProcessingService, VariableProcessingService>();
builder.Services.AddScoped<IGeoService, GeoService>();
builder.Services.AddScoped<IHumanDesignReportBuilder, HumanDesignReportBuilder>();

var app = builder.Build();
await app.Services.CreateScope().ServiceProvider.GetRequiredService<ReferenceDataSeeder>().SeedAsync();

//app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.Run();
