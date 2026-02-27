using Microsoft.EntityFrameworkCore;
using HumanDesign.Infrastructure.Data;
using HumanDesign.Application.Interfaces;
using HumanDesign.Application.Services.Reference;
using HumanDesign.Application.Services.Reports;
using HumanDesign.Application.Services.Diagram;
using HumanDesign.Application.Services.Processing;
using HumanDesign.Application.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Register DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IProspectService, ProspectService>(); 
builder.Services.AddScoped<FileResolver>();
builder.Services.AddScoped<IHumanDesignCalculator, HumanDesignCalculator>();
builder.Services.AddScoped<IReferenceDataService, ReferenceDataService>();
builder.Services.AddScoped<IInterpretationService, InterpretationService>();
builder.Services.AddScoped<ITypeInterpretationService, TypeInterpretationService>();
builder.Services.AddScoped<IVariableProcessingService, VariableProcessingService>();
builder.Services.AddScoped<IGeoService, GeoService>();
builder.Services.AddScoped<IHumanDesignReportBuilder, HumanDesignReportBuilder>();
builder.Services.AddScoped<DiagramModelBuilder>();
builder.Services.AddScoped<IDiagramRenderer, SvgDiagramRenderer>();

var app = builder.Build();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
